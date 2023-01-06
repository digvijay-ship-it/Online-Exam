/*let fatchBtn = document.getElementById("fatchBtn");

fatchBtn.addEventListener('click', buttonClickHandler)
*/

function buttonClickHandler(id) {
    const xhr = new XMLHttpRequest();
    console.log("in Ajax Body");

/*    //
    xhr.onprogress = function () {
        console.log('On progress');
    }
*/

    xhr.onload = function () {
        if (this.status === 200) {
            let a = JSON.parse( this.responseText);
            console.log(this.responseText);
/*            console.log(a["data"][0]["question"]["id"])
*/        }
        else {
            console.log("some error acuured")
        }
    }

    //open the obj
    xhr.open('GET', `https://localhost:44385/Exam/GetAllExam/${id}`, true);
    // Send the Request
    xhr.send();

}