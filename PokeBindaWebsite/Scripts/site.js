$("#test-button").click(() => {
    $.get("/Browse/Card?card=100000", (data, status) => {
        alert("Data: " + data + "\nStatus: " + status);
    });
});

function statusUpdated(event, status) {
    let element = document.getElementById("status-success");
    element.textContent = status.charAt(0).toUpperCase() + status.slice(1);
    setTimeout(function () {
        element.classList.remove('done');
        void element.offsetWidth;
        element.classList.add('done');
        element.classList.remove('show');
    }, 100);
}

function statusUpdating() {
    let element = document.getElementById("status-success");
    element.textContent = "Loading";
    void element.offsetWidth;
    element.classList.add('show')
}

function updateGroupInfo(data, status) {
    let favoriteIcon = document.getElementById("favorite-card-image");
    favoriteIcon.classList.remove("not-favorite");
    favoriteIcon.classList.remove("is-favorite");
    favoriteIcon.classList.add(data.isFavorite ? "is-favorite" : "not-favorite");
    let groupElement = document.getElementById("currently-viewing-group");
    let addButtons = $("#add-dropdown-buttons");
    let removeButtons = $("#remove-dropdown-buttons");

    addButtons.empty();
    if (data.newAddGroups.length == 0) {
        addButtons.append("<h5>Empty</h5>");
    }
    for (i = 0; i < data.newAddGroups.length; i++)
    {
        addButtons.append('<button type="submit" class="dropdown-item view-card-dropdown-item" name="group" value="' + data.newAddGroups[i].id + '">' + data.newAddGroups[i].name + '</button >')
    }
    removeButtons.empty();
    if (data.newRemoveGroups.length == 0) {
        removeButtons.append("<h5>Empty</h5>");
    }
    for (i = 0; i < data.newRemoveGroups.length; i++)
    {
        removeButtons.append('<button type="submit" class="dropdown-item view-card-dropdown-item" name="group" value="' + data.newRemoveGroups[i].id + '">' + data.newRemoveGroups[i].name + '</button >')
    }


    if (groupElement != null && groupElement.getAttribute("value") == data.changedGroup) {
        reloadGroup($("#filter-form").serialize());
    }
}

function reloadGroup(query)
{
    $("#loading-div").attr("style", "");
    $.get("/Collection/Group?" + query, function (data) {
        $("#page-content").html(data);
        $("#loading-div").attr("style", "display:none;");
    });
}

function cardRemoved(dat) {
    let group = $("#currently-viewing-group").attr("value");
    if (group == null);
    else if (group == "all") {
        $("#loading-div").attr("style", "");
        $.get("/Collection/Index", function (data) {
            $("#page-content").html(data);
            $("#loading-div").attr("style", "display:none;");
        });
    }
    else
    {
        reloadGroup($("#filter-form").serialize());
    }
}

function rebindValidation(data) {
    $("form").removeData("validator");
    $("form").removeData("unubtrusiveValidation");
    $.validator.unobtrusive.parse("form");
}

function hideModal(data) {
    $("#modal-popup").modal("hide");
}

$("#modal-popup").on("hidden.bs.modal", function () {
    $("#modal-body").html("");
});