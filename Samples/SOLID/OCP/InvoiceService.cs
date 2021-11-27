using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samples.SOLID.OCP
{
    enum LineItemType
    { 
        Product, Service, Tax
    }

class LineItemValidator
{
    public bool IsValid(LineItem lineItem)
    {
        switch (lineItem.LineItemType)
        {
            case LineItemType.Product:
                return ValidateProduct(lineItem);
            case LineItemType.Service:
                return ValidateService(lineItem);
            case LineItemType.Tax:
                return ValidateTax(lineItem);
            default:
                throw new Exception("unrecognized type");
        }
    }

    private bool ValidateTax(LineItem lineItem)
    {
        return lineItem.Discount == 0;
    }

    private bool ValidateService(LineItem lineItem)
    {
        return lineItem.Description != null;
    }

    private bool ValidateProduct(LineItem lineItem)
    {
        return lineItem.Amount >= 0;
    }
}

    abstract class LineItem 
    {
        public LineItemType LineItemType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
    }

    class ExtendableLineItemValidator
    {
        List<Func<LineItem, bool>> _validations = 
            new List<Func<LineItem, bool>>();

        void AddValidation(Func<LineItem, bool> validation)
        {
            _validations.Add(validation);
        }

        bool Validate(LineItem lineItem)
        {
            return _validations.All(v => v(lineItem));
        }
    }
}
