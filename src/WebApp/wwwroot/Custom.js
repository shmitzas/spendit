// --- Transactions ---
function showAddTransactionModal(trCategories) {
    var categoriesList = "";
    trCategories.forEach((category) => {
        categoriesList += `<option value="${category.name}">${category.name}</option>`;
    });

    var swalOptions = {
        title: "Add transaction",
        html: `<div class='container' style='width:100%'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description'></textarea>
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
    var trDateObj = new Date(trDate);
    var trDateString = trDateObj.toISOString().substring(0, 16);
    var swalOptions = {
        title: "Edit transaction",
        html: `<div class='container' style='width:100%'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description' value=>${trDesc}</textarea>
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
        html: `<div class='container' style='width:100%'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description'></textarea>
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
        cancelButtonText: "Cancel"
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;
                var type = document.getElementById("type").value;
                var startDate = document.getElementById("startDate").value;
                var endDate = document.getElementById("endDate").value;
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

    var trDateObj = new Date(trStartDate);
    var trStartDateString = trDateObj.toISOString().substring(0, 10);

    trDateObj = new Date(trEndDate);
    var trEndDateString = trDateObj.toISOString().substring(0, 10);

    var swalOptions = {
        title: "Edit transaction",
        html: `<div class='container' style='width:100%'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description' value=>${trDesc}</textarea>
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
                                <input type='date' class='form-control' id='startDate' placeholder='Select date' value=${trStartDateString}>
                            </div>
                            <div class="col-6 text-start">
                                <label for='date'>End date</label>
                                <input type='date' class='form-control' id='endDate' placeholder='Select date' value=${trEndDateString}>
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
        cancelButtonText: "Cancel"
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;
                var type = document.getElementById("type").value;
                var startDate = document.getElementById("startDate").value;
                var endDate = document.getElementById("endDate").value;
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
        html: `<div class='container' style='width:100%'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder="Enter description (keep it short)"></textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-8 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter goal amount'>
                            </div>
                            <div class='col-4 text-start'>
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
        cancelButtonText: "Cancel"
    };

    return Swal.fire(swalOptions)
        .then((result) => {
            if (result.isConfirmed) {
                var description = document.getElementById("description").value;
                var amount = document.getElementById("amount").value;
                var currency = document.getElementById("currency").value;
                var date = document.getElementById("date").value;
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

    var trDateObj = new Date(trStartDate);
    var trStartDateString = trDateObj.toISOString().substring(0, 10);

    trDateObj = new Date(trEndDate);
    var trEndDateString = trDateObj.toISOString().substring(0, 10);

    var swalOptions = {
        title: "Edit goal",
        html: `<div class='container' style='width:100%'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description' value=>${trDesc}</textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-8 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter goal amount' value='${trAmount}'>
                            </div>
                            <div class='col-4 text-start'>
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
                                <input type='date' class='form-control' id='startDate' placeholder='Select date' value=${trStartDateString}>
                            </div>
                            <div class="col-6 text-start">
                                <label for='date'>End date</label>
                                <input type='date' class='form-control' id='endDate' placeholder='Select date' value=${trEndDateString}>
                            </div>
                        </div>
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
                var currentAmount = document.getElementById("currentAmount").value;
                var currency = document.getElementById("currency").value;
                var startDate = document.getElementById("startDate").value;
                var endDate = document.getElementById("endDate").value;
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
        html: `<div class='container' style='width:100%'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder="Enter description (keep it short)"></textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-8 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter budget amount'>
                            </div>
                            <div class='col-4 text-start'>
                                <label for='currency'>Currency</label>
                                <input value='EUR' type='text' class='form-control' id='currency' disabled>
                            </div>
                        </div>
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
        html: `<div class='container' style='width:100%'>
                    <div class='form-group mb-3 text-start'>
                        <label for='description'>Description</label>
                        <textarea class='form-control' id='description' placeholder='Enter description' value=>${trDesc}</textarea>
                    </div>
                    <div class='form-group mb-3'>
                        <div class='row'>
                            <div class='col-8 text-start'>
                                <label for='amount'>Amount</label>
                                <input type='number' class='form-control' id='amount' placeholder='Enter budget amount' value='${trAmount}'>
                            </div>
                            <div class='col-4 text-start'>
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
        cancelButtonText: "Cancel"
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
        html: `<div class='container' style='width:100%'>
                    <div class="col-12 pt-2">
                        <table class="table table-hover">
                            <thead class="border-bottom-1">
                                <tr>
                                    <th scope="col" class="col-6 text-start">Description</th>
                                    <th scope="col" class="col-3 text-center text-primary">Date</th>
                                    <th scope="col" class="col-3 text-center text-primary">Amount</th>
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
        width: "50%"
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
        html: `<div class='container' style='width:100%'>
                    <div class="col-12 pt-2">
                        <table class="table table-hover">
                            <thead class="border-bottom-1">
                                <tr>
                                    <th scope="col" class="col-6 text-start">Description</th>
                                    <th scope="col" class="col-3 text-center text-primary">Date</th>
                                    <th scope="col" class="col-3 text-center text-primary">Amount</th>
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
        width: "50%"
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