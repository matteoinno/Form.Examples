//every time that the value of the Mobile Phone field is changed, do this operation
$("#MobilePhone").change(function () {
    //get the value of the MobilePhone field
    var mobNum = $(this).val();
    //the javascript replace uses regex. The \d search for digit characters.
    //the replace get:
    //1. the mobile number value in input
    //2. split the mobile number in three parts (/(\d{3})(\d{3})(\d{4})/).
    //3. reassemble the three parts in the XXX-XXX-XXXX format ("$1-$2-$3").
    formattedMobNum = mobNum.replace(/(\d{3})(\d{3})(\d{4})/, "$1-$2-$3");
    //set the formatted mobile number as the value for the MobilePhone field.
    $(this).val(formattedMobNum);

});

$(document).ready(function () {

    //read the input hidden field
    var errorsList = $("#ErrorsList").val();
    //if the errorsList variable has a value
    if (errorsList) {
        //parse the serialized dictionary
        var errors = JSON.parse(errorsList);   
        //iterate the extracted dictionary
        for (error in errors)
        {
            //create the jQuery identification for the current field
            var input_id = "#" + error;
            //if the field has errors
            if (errors[error] > 0) {
                //climb up to the div container, and add the class has-error. 
                //This way the input field will be highlighted
                $(input_id).parent().parent().addClass('has-error');
            }
            else
            {
                //if the current attribute doesnt have error, do the remove operation 
                //I dont check if the class is already present, because it is possible that the current attribute had an error in the previous iteration.
                $(input_id).parent().parent().removeClass('has-error');
            }
        }
    }

});