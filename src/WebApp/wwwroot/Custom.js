function showAddTransactionModal() {
    var swalOptions = {
        title: "Add transaction",
        html: `<div class='container' style='width:100%'>
            <div class='form-group mb-3 text-start'>
                <label for='description'>Description</label>
                <textarea class='form-control' id='description'></textarea>
            </div>
            <div class='form-group mb-3'>
                <div class='row'>
                    <div class='col-8 text-start'>
                        <label for='amount'>Amount</label>
                        <input type='number' class='form-control' id='amount' placeholder='Enter transaction amount'>
                    </div>
                    <div class='col-4 text-start'>
                        <label for='currency'>Currency</label>
                        <input value='EUR' type='text' class='form-control' id='currency' disabled>
                    </div>
                </div>
            </div>
            <div class='form-group mb-3 text-start'>
                <label for='type'>Type</label>
                <select class='form-select' id='type' aria-label='Expense'>
                    <option selected>Expense</option>
                    <option value='Income'>Income</option>
                </select>
            </div>
            <div class='form-group mb-3 text-start'>
                <label for='date'>Date</label>
                <input type='datetime-local' class='form-control' id='date' placeholder='Select date'>
            </div>
        </div>`,
        showCancelButton: true,
        confirmButtonText: "Save",
        cancelButtonText: "Cancel"
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;
                var type = document.getElementById("type").value;
                var date = document.getElementById("date").value;

                var transactionData = {
                    description: description,
                    amount: amount,
                    currency: currency,
                    type: type,
                    date: date
                };
                return JSON.stringify(transactionData);
            } else {
                return null;
            }
        });
}

function showEditTransactionModal(trDesc, trAmount, trCurrency, trType, trDate) {
    var trDateObj = new Date(trDate);
    var trDateString = trDateObj.toISOString().substring(0, 16);
    var swalOptions = {
        title: "Edit transaction",
        html: `<div class='container' style='width:100%'>
            <div class='form-group mb-3 text-start'>
                <label for='description'>Description</label>
                <textarea class='form-control' id='description' value=>${trDesc}</textarea>
            </div>
            <div class='form-group mb-3'>
                <div class='row'>
                    <div class='col-8 text-start'>
                        <label for='amount'>Amount</label>
                        <input type='number' class='form-control' id='amount' placeholder='Enter transaction amount' value='${trAmount}'>
                    </div>
                    <div class='col-4 text-start'>
                        <label for='currency'>Currency</label>
                        <input value='${trCurrency}' type='text' class='form-control' id='currency' disabled>
                    </div>
                </div>
            </div>
            <div class='form-group mb-3 text-start'>
                <label for='type'>Type</label>
                <select class='form-select' id='type' aria-label='Expense' value ='${trType}'>
                    <option selected>Expense</option>
                    <option value='Income'>Income</option>
                </select>
            </div>
            <div class='form-group mb-3 text-start'>
                <label for='date'>Date</label>
                <input type='datetime-local' class='form-control' id='date' placeholder='Select date' value='${trDateString}'>
            </div>
        </div>`,
        showCancelButton: true,
        confirmButtonText: "Save",
        cancelButtonText: "Cancel"
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;
                var type = document.getElementById("type").value;
                var date = document.getElementById("date").value;

                var transactionData = {
                    description: description,
                    amount: amount,
                    currency: currency,
                    type: type,
                    date: date
                };
                return JSON.stringify(transactionData);
            } else {
                return null;
            }
        });
}