/*var add_more_fields = document.getElementById("add_more_field");

add_more_fields.onclick = function () {
    var newField = document.createElement('input');
    newField.setAttribute('type', 'text');
    newField.setAttribute('name', 'survey_options[]');
    newField.setAttribute('class', 'survey_options');
    newField.setAttribute('siz', 50);
    newField.setAttribute('placeholder', 'Another Field');
    dsurvey_options.appendChild(newField);
}*/

var counter = 4;

function AddButton() {

    var OptionLable = document.createElement("lable")

    var OptionInput = document.createElement("textarea")

    OptionLable.setAttribute("for", "opTextList_" + counter + "_")
    OptionLable.innerHTML = "Newly Added option"

    OptionInput.setAttribute("id", "opTextList_" + counter + "_")
    OptionInput.setAttribute("name", "opTextList[" + counter + "]")
    OptionInput.setAttribute("type", "text")
    OptionInput.setAttribute("value", "")
    OptionInput.setAttribute("rows", "1")
    OptionInput.setAttribute("class", "form-control")


    //textarea asp-for="opTextList[3]" rows="1" class="form-control"

    document.getElementById("add").append(OptionLable)
    document.getElementById("add").append(OptionInput)
    document.getElementById("add").append(document.createElement("br"))

    console.log(counter)
    counter++
}