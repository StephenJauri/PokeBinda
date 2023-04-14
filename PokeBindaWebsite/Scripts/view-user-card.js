$("#test-button").click(() => {
    $.get("/Browse/Card?card=100000", (data, status) => {
        alert("Data: " + data + "\nStatus: " + status);
    });
});