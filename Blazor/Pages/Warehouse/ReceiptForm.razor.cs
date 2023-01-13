using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace Blazor.Pages.Warehouse;

public partial class ReceiptForm : ComponentBase
{
    private FormModel model = new();

    private void ReceiptItems()
    {

    }

    private class FormModel
    {
        [Required(ErrorMessage = "Enter invoice name")]
        public string Invoice { get; set; }

        public FormModel() { }

        public FormModel(string invoice)
        {
            Invoice = invoice;
        }
    }
}