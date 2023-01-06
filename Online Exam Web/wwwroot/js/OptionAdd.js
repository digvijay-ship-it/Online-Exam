var add_more_fields = document.getElementById("add_more_field");



add_more_fields.onclick = function () {
    var newField = document.createElement('input');
    newField.setAttribute('type', 'text');
    newField.setAttribute('name', 'survey_options[]');
    newField.setAttribute('class', 'survey_options');
    newField.setAttribute('siz', 50);
    newField.setAttribute('placeholder', 'Another Field');
    dsurvey_options.appendChild(newField);
}