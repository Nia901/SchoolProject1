async function loadTeachers() {
    let name = document.getElementById("name").value;
    let subject = document.getElementById("subject").value;
    let sortOrder = document.getElementById("sortOrder").value;

    let params = new URLSearchParams({
        name: name,
        subject: subject,
        sortOrder: sortOrder
    });

    let response = await fetch(
        "/Teacher/GetFilterTeachers?" + params
    );

    let teachers = await response.json();

    let tbody = document.getElementById("teachersTable");
    tbody.innerHTML = "";

    teachers.forEach(function (teacher) {
        let row = document.createElement("tr");

        [
            teacher.id,
            teacher.firstName,
            teacher.lastName,
            teacher.age,
            teacher.address,
            teacher.email,
            teacher.phoneNumber,
            teacher.subject,
            teacher.schoolName
        ].forEach(function (value) {
            let cell = document.createElement("td");
            cell.textContent = value ?? "";
            row.appendChild(cell);
        });

        let actions = document.createElement("td");

        ["Edit", "Details", "Delete"].forEach(function (action, index) {
            if (index > 0) {
                actions.appendChild(document.createTextNode(" | "));
            }

            let link = document.createElement("a");
            link.href = "/Teacher/" + action + "/" + teacher.id;
            link.textContent = action;
            actions.appendChild(link);
        });

        row.appendChild(actions);
        tbody.appendChild(row);
    });
}

// Автоматично филтриране
document.getElementById("name")
    .addEventListener("input", loadTeachers);

document.getElementById("subject")
    .addEventListener("input", loadTeachers);

document.getElementById("sortOrder")
    .addEventListener("change", loadTeachers);

// Изчистване на филтрите
document.getElementById("clearFilters")
    .addEventListener("click", function () {
        document.getElementById("name").value = "";
        document.getElementById("subject").value = "";
        document.getElementById("sortOrder").value = "name_asc";

        loadTeachers();
    });
