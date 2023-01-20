document.getElementById("prBtn").disabled = true;


var counter = 0;
var viewdata;

function datafiller(counter) {
    // let opCounter = 0;
    let question = document.getElementById("QueFill")
    let optionTag = document.getElementById("opFill")
    // let op = optionTag.getElementById(1);
    question.innerHTML = viewdata['examdata'][counter]['question']['question']

    let userAns = viewdata["examdata"][counter]["result"]["users_Answer"]
    let Ans = viewdata["examdata"][counter]["result"]["answer"]

    if (userAns === null || userAns !== Ans) {
        const a = document.getElementById("fillDiv")
        a.attributes[1].value = "background-color: palevioletred"
        const UserAction = document.getElementById("UserAction").innerHTML = "you selected no Option"
    }

    else {//when (userAns === Ans)
        //apply green colour in background
        const a = document.getElementById("fillDiv")
        a.attributes[1].value = "background-color:#99ffbb"
    }

    for (let i = 0; i < viewdata["examdata"][counter]["optionsList"].length; i++) {
        let option = viewdata["examdata"][counter]["optionsList"][i]["option"]

        let questionId = viewdata["examdata"][counter]["question"]["id"]
        let optionId = viewdata["examdata"][counter]["optionsList"][i]["id"]

        if (userAns !== null && optionId === userAns) {
            const UserAction = document.getElementById("UserAction").innerHTML = viewdata["examdata"][counter]["optionsList"][i]["option"]
        }
        if (userAns === optionId) {
            document.getElementById(i.toString()).innerHTML = `
            <input type="radio" checked="checked" id="${userAns}" name="${questionId}" value="${optionId}" disabled >
            <label for="${userAns}">${option}</label>
              `
        }
        else {
            document.getElementById(i.toString()).innerHTML = `
              <input type="radio" id="${userAns}" name="${questionId}" value="${optionId}" disabled>
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
    if (counter < viewdata["examdata"].length - 1) {
        buttonEnabler("neBtn")
    }
    else {
        buttonDisabler("neBtn")
    }
}
function prewButtonClicked() {
    counter--
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
}
function buttonDisabler(btnname) {
    //make button disabled
    document.getElementById(btnname).disabled = true;
}
function buttonEnabler(btnname) {
    //make button disabled
    document.getElementById(btnname).disabled = false;
}

function buttonClickHandler(subId, Uid) {
    /*viewdata = await fetch(`https://localhost:44385/User/GetExamResultByAdmin/${subId}/${Uid}`);*/
    const xhr = new XMLHttpRequest();
    document.getElementById("buton").hidden = false;

    console.log(`https://localhost:44385/User/GetExamResultByAdmin?id=${subId}&uId=${Uid}`)
    /*    //
      xhr.onprogress = function () {
          console.log('On progress');
      }
    */
    xhr.onload = function () {
        if (this.status === 200) {
            viewdata = JSON.parse(this.responseText);
            datafiller(0)

            document.getElementById("Percentage").innerHTML = percent;
            document.getElementById("perdetails").hidden = false;
        }
        else {
            console.log("some error acuured")
        }
    }
    xhr.open('GET', `https://localhost:44385/User/GetExamResultByAdmin?id=${subId}&uId=${Uid}`, true);
    xhr.send();
}