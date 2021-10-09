
function validate() {
    let input = document.getElementById("academic-year");
    let year = input.value;
    if (year.length > 9) input.innerHTML = year.substring(0, 7);
    let years = year.split("/");
    if (parseInt(years[0]) + 1 != parseInt(years[1])) {
        input.style.background= "var(--clr-red-light)";
    }
    else {
        input.style.background = "var(--clr-grey-10)";
    }
}

$(document).ready(function () {
        $("#close").click(function () {
            $("article#add").hide();
        })
})

function add(courseID, courseCode) {
    var form = document.getElementById("add");
    form.style.display = "block";
    var hiddenInput = form.getElementsByClassName("courseID");
    hiddenInput[0].setAttribute("value", courseID);
    var header = form.getElementsByTagName("b")[0];
    header.innerText = "Add section to " + courseCode;
}

