using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvProcessor.MultiThread
{
    class FileProcessor
    {
        IWidgetLogic _widgetLogic;
        IWidgetProvider _widgetProvider;
        IWidgetQueueProvider _widgetQueueProvider;

        public FileProcessor(IWidgetLogic logic, IWidgetProvider provider)
        {
            _widgetLogic = logic;
            _widgetProvider = provider;
        }

        internal Task ProcessFile(string filePath)
        {
            var rdr = new CsvReader<Task>(filePath, line =>
            {
                var widget = new Widget(line);
                return Task.Run(() => _widgetLogic.ProcessWidget(widget))
                .ContinueWith(t =>
                {
                    if (t.Exception != null)
                    {
                        throw t.Exception;
                    }
                    return _widgetProvider.Save(widget);
                });
            });

            return Task.WhenAll(rdr.Read());
        }

        internal Task ProcessFileWithSemaphore(string filePath)
        {
            SemaphoreSlim slim = new SemaphoreSlim(10, 10);
            var rdr = new CsvReader<Task>(filePath, line => {
                slim.Wait();
                try
                {
                    var widget = new Widget(line);
                    _widgetLogic.ProcessWidget(widget);
                    return _widgetProvider.Save(widget);
                }
                finally
                {
                    slim.Release();
                }
            });

            return Task.WhenAll(rdr.Read());
        }

        internal Task ProcessFileWithSemaphoreFixed(string filePath)
        {
            SemaphoreSlim slim = new SemaphoreSlim(50, 50);
            var rdr = new CsvReader<Task>(filePath, async line => {
                await slim.WaitAsync();
                try
                {
                    var widget = new Widget(line);
                    _widgetLogic.ProcessWidget(widget);
                    await _widgetProvider.Save(widget);
                }
                finally
                {
                    slim.Release();
                }
            });

            return Task.WhenAll(rdr.Read());
        }


        internal Task ProccessFileBatched(string filePath)
        {
            var rdr = new CsvReader<Widget>(filePath,
                line => new Widget(line));

            var batchTasks =
                from batch in rdr.ReadBatched(10)
                select Task.Run(() =>
                {
                    foreach (var widget in batch)
                    {
                        _widgetLogic.ProcessWidget(widget);
                    }
                    return _widgetProvider.Save(batch);
                });
            return Task.WhenAll(batchTasks);
        }


internal Task ProccessWithMessageQueue(string filePath)
{
    var rdr = new CsvReader<Task>(filePath, line => 
        _widgetQueueProvider.Enqueue(new Widget(line))
    );
    return Task.WhenAll(rdr.Read());
}
    }
    class Widget
    {
        public Widget(string[] fields)
        {

        }
    }


    interface IWidgetLogic
    {
        void ProcessWidget(Widget widget);

    }

    class FakeWidgetLogic : IWidgetLogic
    {
        public void ProcessWidget(Widget widget)
        {
            //Thread.Sleep(10000);
        }
    }

    interface IWidgetProvider
    {
        Task Save(Widget widget);
        Task Save(IEnumerable<Widget> widgets);
    }

    class FakeWidgetProvider : IWidgetProvider
    {
        public Task Save(Widget widget)
        {
            return Task.Delay(60000);
        }

        public Task Save(IEnumerable<Widget> widgets)
        {
            throw new NotImplementedException();
        }
    }

    interface IWidgetQueueProvider
    {
        Task Enqueue(Widget widget);
    }
}
