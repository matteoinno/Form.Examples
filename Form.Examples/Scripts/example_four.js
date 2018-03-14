//initialize the textarea with id 'Html' as a ckeditor instance.
$(document).ready(function () {
    CKEDITOR.replace('Html', {
        //this parameter allows to insert any type of tag inside the editor.
        allowedContent: true
    });
});