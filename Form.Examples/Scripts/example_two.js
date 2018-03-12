function OpenFormModal()
{
    //get the HTML code from the partial view that contains the form.
    //I used a partial view, so the view it is not accessible by the user.
    $.get("/Examples/ExampleTwoForm", function (result) {
        //load the view inside the body of the modal dialog.
        $("#insert-dialog-form-container").html(result);
    });
    //open the modal dialog.
    $("#exampleModal").modal('show');
}

function SubmitForm() {

    //serialize the data from the form.
    var formData = $("#insert-dialog-form").serialize();
    //open the waiting modal: a modal that contains a loading gif.
    $("#waitingModal").modal('show');
    //post the request by JQuery
    $.post('/Examples/ExampleTwo', formData, function (result) {
        //if it returns OK 
        if (result == "OK") {
            //extract the values from the form in an array and create a row to append to the table.
            var formArray = $("#insert-dialog-form").serializeArray();
            var result_row = "";
            result_row = "<tr>"
            for (val in formArray)
            {
                if (formArray[val].name != "__RequestVerificationToken") {                    
                    var cell_value = "<td>" + formArray[val].value + "</td>";
                    result_row += cell_value;
                }
            }
            result_row += "</tr>";
            $("#insert-dialog-tbody").append(result_row);
        }
        else if (result == "KO") {
            //the import failed by exception, open the error modal dialog.
            $("#errorModal").modal('show');
        }
        else {
            $("#insert-dialog-form-container").html(result);
        }
        //once the post request is complete, close the waiting and the form modal.
        $("#waitingModal").modal('hide');
        $("#exampleModal").modal('hide');
    });
    
}

function EditAddressBook(id)
{
    //Open the Edit form in a dialog box. 
    //1. open the waiting modal
    $("#waitingModal").modal('show');
    //2. by the id retrieve the info of the address book and open the edit dialog
    $.post('/Examples/GetAddressBookById', "id="+id, function (data) {

        $("#edit-dialog-form-container").html(data);
        $("#waitingModal").modal('hide');
        $("#exampleModal").modal('show');

    });
}