let cars = [];
let connection = null;

let carIdToUpdate = -1;

getdata();
setupSignalR();
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:61242/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CarCreated", (user, message) => {
        getdata();
    });

    connection.on("CarDeleted", (user, message) => {
        getdata();
    });

    connection.on("CarUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};


async function getdata() {
    await fetch('http://localhost:61242/car')
        .then(x => x.json())
        .then(y => {
            cars = y;
            //console.log(cars);
            display();
        });
}





function display() {
    document.getElementById('resultarea').innerHTML = "";
    cars.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.carID + "</td><td>"
            + t.brandID + "</td><td>"
        + t.orderID + "</td><td>" +
        t.model + "</td><td>" +
            `<button type="button" class="first-button" onclick="remove(${t.carID})">Delete</button>` +
            //`<div class="spacer"></div>` +
            //`<p></p>` +
            `<button type="button" class="second-button" onclick="showupdate(${t.carID})">Update</button>`
            + "</td></tr>";
        console.log(carIdToUpdate);
    });
}

function remove(id) {
    fetch('http://localhost:61242/car/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function showupdate(id) {
    document.getElementById('brandidtoupdate').value = cars.find(t => t['carID'] == id)['brandID'];
    document.getElementById('orderidtoupdate').value = cars.find(t => t['carID'] == id)['orderID'];
    document.getElementById('modelnametoupdate').value = cars.find(t => t['carID'] == id)['model'];
    document.getElementById('updateformdiv').style.display = 'flex';
    carIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let brandid = document.getElementById('brandidtoupdate').value;
    let orderid = document.getElementById('orderidtoupdate').value;
    let model = document.getElementById('modelnametoupdate').value;
    fetch('http://localhost:61242/car', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { brandID: brandid, orderID: orderid, Model: model, carID: carIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let brandid = document.getElementById('brandid').value;
    let orderid = document.getElementById('orderid').value;
    let model = document.getElementById('modelname').value;
    let id =
        fetch('http://localhost:61242/car', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify(
                { brandID: brandid, orderID: orderid, Model: model })
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getdata();
            })
            .catch((error) => { console.error('Error:', error); });
}