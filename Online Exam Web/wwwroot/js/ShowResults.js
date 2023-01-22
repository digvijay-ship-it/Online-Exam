var viewdata;
document.getElementById("GroupDataShow").hidden = true
// redirect to resultview if one is selected












//show data
function FillData(vd) {
    for (let i = 0; i < viewdata['data'].length; i++) {
        let useId = viewdata['data'][i]["userId"]
        let userEmail = viewdata['data'][i]["user_Email"]
        let sub = viewdata['data'][i]["subject_Name"]
        let subId = viewdata['data'][i]["subjectId"]
        let per = viewdata['data'][i]["percentage"]
        //tbodyOfGroupData
        var tableRow = document.createElement("tr")
        var TableData1 = document.createElement("td")
        var TableData2 = document.createElement("td")
        var TableData3 = document.createElement("td")
        var button4TD4 = document.createElement("a")

        //in this one enter a link with arguments like this
        //<a class="btn btn-primary" href="/Question/Edit/12">Edit Question Or Add Options</a>
        //< a onclick = "buttonClickHandler(10,6)" class="btn btn-primary" > ResultWIthApi</a >
        //attemptedExamAdmin.js
        //console.log(`https://localhost:44385/User/GetExamResultByAdmin?id=${subId}&uId=${Uid}`)
        var TableData4 = document.createElement("td")

        //insert values in these TableData
        TableData1.innerHTML = `${userEmail}`
        TableData2.innerHTML = `${sub}`
        TableData3.innerHTML = `${per}`

        //<a onclick="buttonClickHandler(11,6)" class="btn btn-primary">ResultWIthApi</a>
        button4TD4.setAttribute("onclick", `buttonClickHandler(${subId},${useId})`)
        button4TD4.setAttribute("class", "btn btn-primary")
        button4TD4.innerHTML = "Result In Detail"

        //append button in TableData4
        TableData4.append(button4TD4)
        //apend in tr
        tableRow.append(TableData1, TableData2, TableData3, TableData4)
        document.getElementById("tbodyOfGroupData").append(tableRow)
    }
}


function showData() {

    const xhr = new XMLHttpRequest()
    
    var date1 = $("#date1").val()
    var date2 = $("#date2").val()
    /*console.log(typeof date1)
    console.log(date1)*/

    if (false) {
        //if data1 and data2 is falsy
        console.log(date1)
        location.href = `https://localhost:44385/Result`;
    }

    xhr.onload = function () {
        if (this.status === 404) {
            // make it work where user can go to this index
            location.href = `https://localhost:44385/Result`;
        }
        if (this.status === 200) {
            //hide table 
            document.getElementById("searchBox").hidden = false;
            //reset dates input fields
            document.getElementById("tbodyOfGroupData").innerHTML=''

            document.getElementById("table").hidden = true;
            document.getElementById("GroupDataShow").hidden = false;
            
            viewdata = JSON.parse(this.response)

            FillData(viewdata)
        }

    }
    //Result?SearchString1=2023-01-16&SearchString2=2023-01-19
    xhr.open("GET", `https://localhost:44385/Result/ResultGropupdataShow?SearchString1=${date1}&SearchString2=${date2}`, true)
    xhr.send()

}