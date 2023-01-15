document.getElementById("prBtn").disabled = true;

var counter = 0;

var viewdata;

function userAnswer(c, i) {
    viewdata["examdata"][c]["result"]["users_Answer"] = viewdata["examdata"][c]["optionsList"][i]["id"]

}
function datafiller(counter) {
    // let opCounter = 0;
    let question = document.getElementById("QueFill")
    let optionTag = document.getElementById("opFill")
    // let op = optionTag.getElementById(1);
    question.innerHTML = viewdata['examdata'][counter]['question']['question']
    //subid
    viewdata["examdata"][counter]["result"]["subjectId"] = viewdata["examdata"][counter]["question"]["subjectId"]
    //queid
    viewdata["examdata"][counter]["result"]["questionId"] = viewdata["examdata"][counter]["question"]["id"]
    //answer
    viewdata["examdata"][counter]["result"]["answer"] = viewdata["examdata"][counter]["question"]["answer"]

    for (let i = 0; i < viewdata["examdata"][counter]["optionsList"].length; i++) {
        let option = viewdata["examdata"][counter]["optionsList"][i]["option"]
        let userAns = viewdata["examdata"][counter]["result"]["users_Answer"]
        let questionId = viewdata["examdata"][counter]["question"]["id"]
        let optionId = viewdata["examdata"][counter]["optionsList"][i]["id"]
        if (userAns === optionId) {
            document.getElementById(i.toString()).innerHTML = `
    <input type="radio" checked="checked" onclick="userAnswer(${counter},${i})" id="${userAns}" name="${questionId}" value="${optionId}">
    <label for="${userAns}">${option}</label>
      `
        }
        else {
            document.getElementById(i.toString()).innerHTML = `
      <input type="radio" onclick="userAnswer(${counter},${i})" id="${userAns}" name="${questionId}" value="${optionId}">
      <label for="${userAns}">${option}</label>
        `
        }
    }

}
function nextButtonClicked() {
    counter++
    datafiller(counter)
    if (counter > 0) {
        buttonEnabler("prBtn")
    }
    if (counter < viewdata["examdata"].length-1) {
        buttonEnabler("neBtn")
    }
    else {
        buttonDisabler("neBtn")
        document.getElementById("sub").hidden = false;
    }
    console.log(viewdata)
}
function prewButtonClicked() {
    counter--
    document.getElementById("sub").hidden = true;
    datafiller(counter)
    if (counter < viewdata["examdata"].length) {
        buttonEnabler("neBtn")
    }
    if (counter == 0) {
        buttonDisabler("prBtn")
    }
    else {
        buttonEnabler("prBtn")
    }
    console.log(viewdata)
}
function buttonDisabler(btnname) {
    //make prew button disabled
    document.getElementById(btnname).disabled = true;
}
function buttonEnabler(btnname) {
    //make prew button disabled
    document.getElementById(btnname).disabled = false;
}

function buttonClickHandler(subId) {
    const xhr = new XMLHttpRequest();
    document.getElementById("fatchBtn").hidden = true
    document.getElementById("buton").hidden = false
    document.getElementById("sub").hidden = true;
    document.getElementById("examInst").hidden = true;
    /*    //
      xhr.onprogress = function () {
          console.log('On progress');
      }
    */
    xhr.onload = function () {
        if (this.status === 200) {
            viewdata = JSON.parse(this.responseText);
            datafiller(0) //viewdata[0]['question']['question']
        }
        else {
            console.log("some error acuured")
        }
    }
    xhr.open('GET', `https://localhost:44385/Exam/GetAllExam/${subId}`, true);
    xhr.send();
}

$("#sub").click(function () {
    let resultArray = []
    for (let i = 0; i <= viewdata["examdata"].length - 1; i++) {
        resultArray.push(viewdata["examdata"][i]['result'])
    }
    //{ DataInString: DataInStringFormat }
    let resultInArrayString = JSON.stringify(resultArray)
    $.post(`https://localhost:44385/Exam/AttemptApi`, { DataInString: resultInArrayString },
        function (data, status) {
            alert("Data: " + data + "\nStatus: " + status);
        });
    //create an empty array for result
    //then run loop on original jason
    //and append result one by one in to that array
    //then send

});
/*

function SubmitData() {
    console.log("dataposting")

    const pxhr = new XMLHttpRequest();
    pxhr.getResponseHeader('Content-type', 'application/json');
    pxhr.open('POST', `https://localhost:44385/Exam/AttemptApi/${viewdata}`, true);
    console.log(JSON.stringify(viewdata));
    pxhr.send(viewdata);
}*/