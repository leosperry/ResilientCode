using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Samples.CatchAndThrow
{
    class ExceptionHandling
    {
        interface ILogger
        {
            void Log(string messagge);
            void LogException(Exception e, string message);
        }
        void DoWork() { }
        bool ConditionHasError() { return default; }
        ILogger _logger;

void HandleExceptionConceal1()
{
    try
    {
        DoWork();
    }
    catch
    {
        _logger.Log("a problem occured doing the work");
        throw new Exception("a problem occurred doing the work");
    }
}

void HandleExceptionConceal2()
{
    try
    {
        DoWork();
    }
    catch (Exception ex)
    {
        _logger.Log(ex.Message);
        throw new Exception("a problem occurred doing the work");
    }
}

void HandleExceptionReveal1()
{
    try
    {
        DoWork();
    }
    catch (Exception ex)
    {
        _logger.LogException(ex, "a problem occurred doing the work");
        throw;
    }
}

public void HandleExceptionReveal2()
{
    try
    {
        DoWork();
    }
    catch (Exception ex)
    {
        _logger.LogException(ex, "a problem occurred doing the work");
        throw new Exception("a problem occurred doing the work", ex);
    }
}


    }

    class Consuer
    {
        ExceptionHandling _handler;

public void Consume()
{
    try
    {
        _handler.HandleExceptionReveal2();
    }
    catch (Exception ex) when 
        (ex.InnerException != null && 
        ex.InnerException.GetType() == typeof(SqlException))
    {
        // handle sql exception
        throw;
    }
}
    }
}
