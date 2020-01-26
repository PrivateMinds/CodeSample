$(document).ready(function ()
{
    //This is a javascript/jquery save data method that uses ajax to post data to the server. 
    //This function calls the SaveEmployeeData method in the SampleCode controller.
    $('#btnSaveEmployeeInformation').click(function (e)
    {
        var foundempty = false;
        var NewEmployeeInformationListObject = []; //create an array object

            //validate required fields.
            if ($('#txtFirstName').val() == "") 
            {
                $('#txtFirstName').css('background', '#ffd9d9') //Flag the field required by highlighting it pink.
                $('#txtFirstName' ).focus();//Put focus on the field.

                foundempty = true;
            }
            if ($('#txtLastName').val() == "") 
            {
                $('#txtLastName').css('background', '#ffd9d9') //Flag the field required by highlighting it pink.
                $('#txtFirstName' ).focus();//Put focus on the field.

                foundempty = true;
            }

            if ($('#txtDOB').val() == "") 
            {
                $('#txtDOB').css('background', '#ffd9d9') //Flag the field required by highlighting it pink.
                $('#txtDOB').focus();//Put focus on the field.

                foundempty = true;
            }

        });

      //if any of the required fields are blank, return.
        if (foundempty) 
        {
            return false;
        }
    
      //Add data into your array object
         NewEmployeeInformationListObject.push
        ({
            ID: 0,
            FirstName: $("#txtFirstName").val(),
            LastName:  $('#txtLastName').val(),
            txtDOB: $('#txtDOB').val(),
           
        });

      //Pass the object to the controller for saving.
        ShowWait();
        var URL = "SampleCode/SaveEmployeeData";
        $.ajax({
            type: 'POST',
            async: false,
            url: URL,
            dataType: 'json',
            data: { NewEmployeeInformationList: NewEmployeeInformationListObject, },
            success: function (data) 
            {
              //Let the user know if data was successfully saved.
            
            },
            error: function (args) {
                var $dialog = $('<div>Error occured while saving employee information.</div>')
               .dialog
               ({
                   title: 'ERROR!',
                   width: 300,
                   height: 200,
                   buttons:
                   [
                   {
                       text: "Close", click: function () {
                           $dialog.dialog('close');
                           // You can do anything else here that needs to be done.
                       }
                   }]
               });

            }
        });
    });
