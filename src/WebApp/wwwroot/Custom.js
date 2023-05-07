// --- Transactions ---
function showAddTransactionModal(trCategories) {
    var categoriesList = "";
    trCategories.forEach((category) => {
        categoriesList += `<option value="${category.name}">${category.name}</option>`;
    });

    var swalOptions = {
        title: "Add transaction",
        html: `<div class='container'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description'></textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-7 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter transaction amount'>
                            </div>
                            <div class='col-5 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='EUR' type='text' class='form-control' id='currency' disabled>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3'>
                        <div class="row">
                            <div class="col-5 text-start">
                                <label for='type'>Type</label>
                                <select class='form-select' id='type' aria-label='Expense'>
                                    <option value='Expense' selected>Expense</option>
                                    <option value='Income'>Income</option>
                                </select>
                            </div>
                            <div class="col-7 text-start">
                                <label for='categories'>Category</label>
                                <select class='form-select' id='categories' aria-label='Category'>
                                    ${categoriesList}
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3 text-start'>
                        <label for='date'>Date</label>
                        <input type='datetime-local' class='form-control' id='date' placeholder='Select date'>
                    </div>
                </div>`,
        showCancelButton: true,
        confirmButtonText: "Save",
        cancelButtonText: "Cancel",
        allowOutsideClick: false
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;
                var type = document.getElementById("type").value;
                var date = new Date(document.getElementById("date").value).toLocaleString();
                var category = document.getElementById("categories").value;

                var transactionData = {
                    description: description,
                    amount: amount,
                    currency: currency,
                    type: type,
                    date: date,
                    category: category
                };
                return JSON.stringify(transactionData);
            } else {
                return null;
            }
        });
}

function showEditTransactionModal(trDesc, trAmount, trCurrency, trType, trOtherType, trDate, trCurrentCategory, trCategories, trCurrentBudget, trBudgets) {
    var categoriesList = "";
    trCategories.forEach((category) => {
        categoriesList += `<option value="${category.name}">${category.name}</option>`;
    });
    var budgetsList = "";
    trBudgets.forEach((budget) => {
        budgetsList += `<option value="${budget.description}">${budget.description}</option>`;
    });

    var date = new Date(trDate);
    trDate = date.toISOString().slice(0, 16);

    var swalOptions = {
        title: "Edit transaction",
        html: `<div class='container'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description' value=>${trDesc}</textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-7 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter transaction amount' value='${trAmount}'>
                            </div>
                            <div class='col-5 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='${trCurrency}' type='text' class='form-control' id='currency' disabled>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3'>
                        <div class="row">
                            <div class="col-5 text-start">
                                <label for='type'>Type</label>
                                <select class='form-select' id='type' aria-label='Expense''>
                                    <option selected>${trType}</option>
                                    <option value='${trOtherType}'>${trOtherType}</option>
                                </select>
                            </div>
                            <div class="col-7 text-start">
                                <label for='category'>Category</label>
                                <select class='form-select' id='categories' aria-label='Category'>
                                    <option selected>${trCurrentCategory}</option>
                                    ${categoriesList}
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3'>
                        <div class="row">
                            <div class="col-10 text-start">
                                <label for='budgets'>Budget</label>
                                <select class='form-select' id='budgets' aria-label='Budget'>
                                    <option selected>${trCurrentBudget}</option>
                                    ${budgetsList}
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3 text-start'>
                        <label for='date'>Date</label>
                        <input type='datetime-local' class='form-control' id='date' placeholder='Select date' value='${trDate}'>
                    </div>
                </div>`,
        showCancelButton: true,
        confirmButtonText: "Save",
        cancelButtonText: "Cancel",
        allowOutsideClick: false
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;
                var type = document.getElementById("type").value;
                var date = new Date(document.getElementById("date").value).toISOString();
                var category = document.getElementById("categories").value;
                var budget = document.getElementById("budgets").value;

                var transactionData = {
                    description: description,
                    amount: amount,
                    currency: currency,
                    type: type,
                    date: date,
                    category: category,
                    budget: budget
                };
                return JSON.stringify(transactionData);
            } else {
                return null;
            }
        });
}

// --- Recurring Transactions ---

function showAddRecurringTransactionModal(trCategories, trFrequencies) {
    var categoriesList = "";
    var frequencyList = "";

    trCategories.forEach((category) => {
        categoriesList += `<option value="${category.name}">${category.name}</option>`;
    });
    trFrequencies.forEach((frequency) => {
        frequencyList += `<option value="${frequency}">${frequency}</option>`;
    });

    var swalOptions = {
        title: "Add transaction",
        html: `<div class='container'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description'></textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-7 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter transaction amount'>
                            </div>
                            <div class='col-5 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='EUR' type='text' class='form-control' id='currency' disabled>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3'>
                        <div class="row">
                            <div class="col-5 text-start">
                                <label for='type'>Type</label>
                                <select class='form-select' id='type' aria-label='Expense'>
                                    <option selected>Expense</option>
                                    <option value='Income'>Income</option>
                                </select>
                            </div>
                            <div class="col-7 text-start">
                                <label for='category'>Category</label>
                                <select class='form-select' id='categoryy' aria-label='Category'>
                                    ${categoriesList}
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3 text-start'>
                        <div class="row">
                            <div class="col-6 text-start">
                                <label for='date'>Start date</label>
                                <input type='date' class='form-control' id='startDate' placeholder='Select date'>
                            </div>
                            <div class="col-6 text-start">
                                <label for='date'>End date</label>
                                <input type='date' class='form-control' id='endDate' placeholder='Select date'>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3'>
                        <div class="row">
                            <div class="col-12 text-start">
                                <label for='frequency'>Frequency</label>
                                <select class='form-select' id='frequency' aria-label='Frequency'>
                                    ${frequencyList}
                                </select>
                            </div>
                        </div>
                    </div>
                </div>`,
        showCancelButton: true,
        confirmButtonText: "Save",
        cancelButtonText: "Cancel",
        allowOutsideClick: false
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;
                var type = document.getElementById("type").value;
                var startDate = new Date(document.getElementById("startDate").value).toISOString();
                var endDate = new Date(document.getElementById("endDate").value).toISOString();
                var category = document.getElementById("categoryy").value;
                var frequency = document.getElementById("frequency").value;

                var transactionData = {
                    description: description,
                    amount: amount,
                    currency: currency,
                    type: type,
                    startDate: startDate,
                    endDate: endDate,
                    category: category,
                    frequency: frequency
                };
                return JSON.stringify(transactionData);
            } else {
                return null;
            }
        });
}

function showEditRecurringTransactionModal(trDesc, trAmount, trCurrency, trType, trOtherType, trStartDate, trEndDate, trCurrentCategory, trCategories, trCurrentFrequency, trFrequencies) {
    var categoriesList = "";
    var frequencyList = "";

    trCategories.forEach((category) => {
        categoriesList += `<option value="${category.name}">${category.name}</option>`;
    });
    trFrequencies.forEach((frequency) => {
        frequencyList += `<option value="${frequency}">${frequency}</option>`;
    });

    var startDate = new Date(trStartDate);
    trStartDate = startDate.toISOString().slice(0, 16);
    var endDate = new Date(trEndDate);
    trEndDate = endDate.toISOString().slice(0, 16);

    var swalOptions = {
        title: "Edit transaction",
        html: `<div class='container'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description' value=>${trDesc}</textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-7 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter transaction amount' value='${trAmount}'>
                            </div>
                            <div class='col-5 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='${trCurrency}' type='text' class='form-control' id='currency' disabled>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3'>
                        <div class="row">
                            <div class="col-5 text-start">
                                <label for='type'>Type</label>
                                <select class='form-select' id='type' aria-label='Expense''>
                                    <option selected>${trType}</option>
                                    <option value='${trOtherType}'>${trOtherType}</option>
                                </select>
                            </div>
                            <div class="col-7 text-start">
                                <label for='category'>Category</label>
                                <select class='form-select' id='ecategory' aria-label='Category'>
                                    <option selected>${trCurrentCategory}</option>
                                    ${categoriesList}
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3 text-start'>
                        <div class="row">
                            <div class="col-6 text-start">
                                <label for='date'>Start date</label>
                                <input type='date' class='form-control' id='startDate' placeholder='Select date' value=${trStartDate}>
                            </div>
                            <div class="col-6 text-start">
                                <label for='date'>End date</label>
                                <input type='date' class='form-control' id='endDate' placeholder='Select date' value=${trEndDate}>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3'>
                        <div class="row">
                            <div class="col-12 text-start">
                                <label for='frequency'>Frequency</label>
                                <select class='form-select' id='frequency' aria-label='Frequency'>
                                    <option selected>${trCurrentFrequency}</option>
                                    ${frequencyList}
                                </select>
                            </div>
                        </div>
                    </div>
                </div>`,
        showCancelButton: true,
        confirmButtonText: "Save",
        cancelButtonText: "Cancel",
        allowOutsideClick: false
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;
                var type = document.getElementById("type").value;
                var startDate = new Date(document.getElementById("startDate").value).toISOString();
                var endDate = new Date(document.getElementById("endDate").value).toISOString();
                var category = document.getElementById("ecategory").value;
                var frequency = document.getElementById("frequency").value;

                var transactionData = {
                    description: description,
                    amount: amount,
                    currency: currency,
                    type: type,
                    startDate: startDate,
                    endDate: endDate,
                    category: category,
                    frequency: frequency
                };
                return JSON.stringify(transactionData);
            } else {
                return null;
            }
        });
}

// --- Goals ---

function showAddGoalModal(trCategories) {
    var categoriesList = "";
    trCategories.forEach((category) => {
        categoriesList += `<option value="${category.name}">${category.name}</option>`;
    });

    var swalOptions = {
        title: "Add goal",
        html: `<div class='container'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder="Enter description (keep it short)"></textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-7 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter goal amount'>
                            </div>
                            <div class='col-5 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='EUR' type='text' class='form-control' id='currency' disabled>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3 text-start'>
                        <label for='category'>Category</label>
                        <select class='form-select' id='category' aria-label='Category'>
                            ${categoriesList}
                        </select>
                    </div>
                    <div class='form-group mb-3 text-start'>
                        <label for='date'>End date</label>
                        <input type='date' class='form-control' id='date' placeholder='Select date'>
                    </div>
                </div>`,
        showCancelButton: true,
        confirmButtonText: "Save",
        cancelButtonText: "Cancel",
        allowOutsideClick: false
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;
                var date = new Date(document.getElementById("date").value).toISOString();
                var category = document.getElementById("category").value;

                var transactionData = {
                    description: description,
                    amount: amount,
                    currency: currency,
                    date: date,
                    category: category
                };
                return JSON.stringify(transactionData);
            } else {
                return null;
            }
        });
}

function showEditGoalModal(trDesc, trAmount, trCurrentAmount, trCurrency, trStartDate, trEndDate, trCurrentCategory, trCategories) {
    var categoriesList = "";

    trCategories.forEach((category) => {
        categoriesList += `<option value="${category.name}">${category.name}</option>`;
    });

    var startDate = new Date(trStartDate);
    trStartDate = startDate.toISOString().slice(0, 10);
    var endDate = new Date(trEndDate);
    trEndDate = endDate.toISOString().slice(0, 10);
    console.log(trStartDate, trEndDate);

    var swalOptions = {
        title: "Edit goal",
        html: `<div class='container'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description'>${trDesc}</textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-7 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter goal amount' value='${trAmount}'>
                            </div>
                            <div class='col-5 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='${trCurrency}' type='text' class='form-control' id='currency' disabled>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-8 text-start'>
                                <label for='currentAmount'>Current amount</label>
                                <input type='number' class='form-control' id='currentAmount' placeholder='Enter current goal amount' value='${trCurrentAmount}'>
                            </div>
                            <div class='col-4 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='${trCurrency}' type='text' class='form-control' id='currency' disabled>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3 text-start'>
                        <label for='category'>Category</label>
                        <select class='form-select' id='category' aria-label='Category'>
                            <option selected>${trCurrentCategory}</option>
                            ${categoriesList}
                        </select>
                    </div>
                    <div class='form-group mb-3 text-start'>
                        <div class="row">
                            <div class="col-6 text-start">
                                <label for='date'>Start date</label>
                                <input type='date' class='form-control' id='startDate' placeholder='Select date' value=${trStartDate}>
                            </div>
                            <div class="col-6 text-start">
                                <label for='date'>End date</label>
                                <input type='date' class='form-control' id='endDate' placeholder='Select date' value=${trEndDate}>
                            </div>
                        </div>
                    </div>
                </div>`,
        showCancelButton: true,
        confirmButtonText: "Save",
        cancelButtonText: "Cancel",
        allowOutsideClick: false
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currentAmount = document.getElementById("currentAmount").value;
                var currency = document.getElementById("currency").value;
                var startDate = new Date(document.getElementById("startDate").value).toISOString();
                var endDate = new Date(document.getElementById("endDate").value).toISOString();
                var category = document.getElementById("category").value;

                var transactionData = {
                    description: description,
                    amount: amount,
                    currentAmount : currentAmount,
                    currency: currency,
                    startDate: startDate,
                    endDate: endDate,
                    category: category,
                };
                return JSON.stringify(transactionData);
            } else {
                return null;
            }
        });
}

// --- Budgets ---

function showAddBudgetModal() {
    var swalOptions = {
        title: "Add budget",
        html: `<div class='container'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder="Enter description (keep it short)"></textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-7 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter budget amount'>
                            </div>
                            <div class='col-5 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='EUR' type='text' class='form-control' id='currency' disabled>
                            </div>
                        </div>
                    </div>
                </div>`,
        showCancelButton: true,
        confirmButtonText: "Save",
        cancelButtonText: "Cancel",
        allowOutsideClick: false
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;

                var transactionData = {
                    description: description,
                    amount: amount,
                    currency: currency,
                };
                return JSON.stringify(transactionData);
            } else {
                return null;
            }
        });
}

function showEditBudgetModal(trDesc, trAmount, trCurrentAmount, trCurrency) {
    var swalOptions = {
        title: "Edit budget",
        html: `<div class='container'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description' value=>${trDesc}</textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-7 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter budget amount' value='${trAmount}'>
                            </div>
                            <div class='col-5 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='${trCurrency}' type='text' class='form-control' id='currency' disabled>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-8 text-start'>
                                <label for='currentAmount'>Current amount</label>
                                <input type='number' class='form-control' id='currentAmount' placeholder='Enter current budget amount' value='${trCurrentAmount}'>
                            </div>
                            <div class='col-4 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='${trCurrency}' type='text' class='form-control' id='currency' disabled>
                            </div>
                        </div>
                    </div>
                </div>`,
        showCancelButton: true,
        confirmButtonText: "Save",
        cancelButtonText: "Cancel",
        allowOutsideClick: false
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currentAmount = document.getElementById("currentAmount").value;
                var currency = document.getElementById("currency").value;

                var transactionData = {
                    description: description,
                    amount: amount,
                    currentAmount: currentAmount,
                    currency: currency
                };
                return JSON.stringify(transactionData);
            } else {
                return null;
            }
        });
}
function showBudgetTransactionsModal(transactions, budgetName) {
    var transactionList = "";
    transactions.forEach((tr) => {
        var trDateObj = new Date(tr.createdAt);
        var trDateString = trDateObj.toISOString().replace(/T/, ' ').replace(/\..+/, '').slice(0, -3);
        transactionList += `
                            <tr class="align-middle">
                                <td class="text-start">${tr.description}</td>
                                <td class="text-center">${trDateString}</td>
                                <td class="text-center text-danger">-${tr.amount}${tr.currency}</td>
                            </tr>
                          `;
    });
    if (transactionList.length == 0) transactionList = "<tr><td colspan='12' class='text-center'>No transactions found</td></tr>";

    var swalOptions = {
        title: `Transactions in ${budgetName}`,
        html: `<div class='container'>
                    <div class="table-responsive-xl pt-2">
                        <table class="table table-hover">
                            <thead class="border-bottom-1">
                                <tr>
                                    <th style="min-width: 150px" scope="col" class="col-6 text-start">Description</th>
                                    <th style="min-width: 160px" scope="col" class="col-3 text-center text-primary">Date</th>
                                    <th style="min-width: 150px" scope="col" class="col-3 text-center text-primary">Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                                ${transactionList}
                            </tbody>
                        </table>
                    </div>
                </div>`,
        showCancelButton: false,
        showSaveButton: false,
        allowOutsideClick: false
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                return null;
            } else {
                return null;
            }
        });
}

// --- Insights ---
function showInsightsModal(transactions, categoryName) {
    var transactionList = "";
    transactions.forEach((tr) => {
        var trDateObj = new Date(tr.createdAt);
        var trDateString = trDateObj.toISOString().replace(/T/, ' ').replace(/\..+/, '').slice(0, -3);
        transactionList += `
                            <tr class="align-middle">
                                <td class="text-start">${tr.description}</td>
                                <td class="text-center">${trDateString}</td>
                                <td class="text-center text-danger">-${tr.amount}${tr.currency}</td>
                            </tr>
                          `;
    });
    if (transactionList.length == 0) transactionList = "<tr><td colspan='12' class='text-center'>No transactions found</td></tr>";

    var swalOptions = {
        title: `Transactions in ${categoryName}`,
        html: `<div class='container'>
                    <div class="table-responsive-xl pt-2">
                        <table class="table table-hover">
                            <thead class="border-bottom-1">
                                <tr>
                                    <th style="min-width: 150px" scope="col" class="col-6 text-start">Description</th>
                                    <th style="min-width: 160px" scope="col" class="col-3 text-center text-primary">Date</th>
                                    <th style="min-width: 150px" scope="col" class="col-3 text-center text-primary">Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                                ${transactionList}
                            </tbody>
                        </table>
                    </div>
                </div>`,
        showCancelButton: false,
        showSaveButton: false,
        allowOutsideClick: false
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                return null;
            } else {
                return null;
            }
        });
}

// --- Bills ---
function showAddBillModal(trCategories) {
    var categoriesList = "";
    trCategories.forEach((category) => {
        categoriesList += `<option value="${category.name}">${category.name}</option>`;
    });

    var swalOptions = {
        title: "Add bill reminder",
        html: `<div class='container'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description'></textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-7 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter bill amount'>
                            </div>
                            <div class='col-5 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='EUR' type='text' class='form-control' id='currency' disabled>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3'>
                        <div class="row">
                            <div class="col-6 text-start">
                                <label for='dueDate'>Due date</label>
                                <input type='datetime-local' class='form-control' id='dueDate' placeholder='Select date'>
                            </div>
                            <div class="col-6 text-start">
                                <label for='categories'>Category</label>
                                <select class='form-select' id='categories' aria-label='Category'>
                                    ${categoriesList}
                                </select>
                            </div>
                        </div>
                    </div>
                    <hr class="mt-5">
                    <div class='form-group mb-3'>
                        <div class="row mb-3">
                            <div class="col-12 text-start mb-3">Set reminders:</div>
                            <div class="col-8 text-start">
                                <label for='reminder1'>Reminder 1</label>
                                <input type='datetime-local' class='form-control' id='reminder1' placeholder='Select date'>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-8 text-start">
                                <label for='reminder2'>Reminder 2</label>
                                <input type='datetime-local' class='form-control' id='reminder2' placeholder='Select date'>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-8 text-start">
                                <label for='reminder3'>Reminder 3</label>
                                <input type='datetime-local' class='form-control' id='reminder3' placeholder='Select date'>
                            </div>
                        </div>
                    </div>
                </div>`,
        showCancelButton: true,
        confirmButtonText: "Save",
        cancelButtonText: "Cancel",
        allowOutsideClick: false
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;
                var dueDate = document.getElementById("dueDate").value;
                var category = document.getElementById("categories").value;
                var reminder1 = document.getElementById("reminder1").value;
                var reminder2 = document.getElementById("reminder2").value;
                var reminder3 = document.getElementById("reminder3").value;

                var reminders = [];
                reminders.push(reminder1);
                reminders.push(reminder2);
                reminders.push(reminder3);

                var transactionData = {
                    description: description,
                    amount: amount,
                    currency: currency,
                    dueDate: dueDate,
                    reminders: JSON.stringify(reminders),
                    category: category
                };
                return JSON.stringify(transactionData);
            } else {
                return null;
            }
        });
}

function showEditBillModal(trDesc, trAmount, trCurrency, trDueDate, trCurrentCategory, trCategories, trReminders) {
    var categoriesList = "";
    trCategories.forEach((category) => {
        categoriesList += `<option value="${category.name}">${category.name}</option>`;
    });

    var reminders = [];
    trReminders.forEach((reminder) => {
        var reminder = reminder.slice(0, 16);
        reminders.push(reminder);
    });

    var date = new Date(trDueDate);
    trDueDate = date.toISOString().slice(0,16);

    var swalOptions = {
        title: "Add bill reminder",
        html: `<div class='container'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description'>${trDesc}</textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-7 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter bill amount' value=${trAmount}>
                            </div>
                            <div class='col-5 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='EUR' type='text' class='form-control' id='currency' disabled value=${trCurrency}>
                            </div>
                        </div>
                    </div>
                    <div class='form-group mb-3'>
                        <div class="row">
                            <div class="col-6 text-start">
                                <label for='dueDate'>Due date</label>
                                <input type='datetime-local' class='form-control' id='dueDate' placeholder='Select date' value=${trDueDate}>
                            </div>
                            <div class="col-6 text-start">
                                <label for='categories'>Category</label>
                                <select class='form-select' id='categories' aria-label='Category'>
                                    <option selected>${trCurrentCategory}</option>
                                    ${categoriesList}
                                </select>
                            </div>
                        </div>
                    </div>
                    <hr class="mt-5">
                    <div class='form-group mb-3'>
                        <div class="row mb-3">
                            <div class="col-12 text-start mb-3">Set reminders:</div>
                            <div class="col-8 text-start">
                                <label for='reminder1'>Reminder 1</label>
                                <input type='datetime-local' class='form-control' id='reminder1' placeholder='Select date' value=${reminders[0]}>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-8 text-start">
                                <label for='reminder2'>Reminder 2</label>
                                <input type='datetime-local' class='form-control' id='reminder2' placeholder='Select date' value=${reminders[1]}>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-8 text-start">
                                <label for='reminder3'>Reminder 3</label>
                                <input type='datetime-local' class='form-control' id='reminder3' placeholder='Select date' value=${reminders[2]}>
                            </div>
                        </div>
                    </div>
                </div>`,
        showCancelButton: true,
        confirmButtonText: "Save",
        cancelButtonText: "Cancel",
        allowOutsideClick: false
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;
                var dueDate = (document.getElementById("dueDate").value);
                var category = document.getElementById("categories").value;
                var reminder1 = (document.getElementById("reminder1").value);
                var reminder2 = (document.getElementById("reminder2").value);
                var reminder3 = (document.getElementById("reminder3").value);

                var reminders = [];
                reminders.push(reminder1);
                reminders.push(reminder2);
                reminders.push(reminder3);

                var transactionData = {
                    description: description,
                    amount: amount,
                    currency: currency,
                    dueDate: dueDate,
                    reminders: JSON.stringify(reminders),
                    category: category
                };
                return JSON.stringify(transactionData);
            } else {
                return null;
            }
        });
}