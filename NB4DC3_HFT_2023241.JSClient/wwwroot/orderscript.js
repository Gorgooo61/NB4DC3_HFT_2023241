let orders = [];
let connection = null;

let orderIdToUpdate = -1;

getdata();
setupSignalR();
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:61242/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("OrderCreated", (user, message) => {
        getdata();
    });

    connection.on("OrderDeleted", (user, message) => {
        getdata();
    });

    connection.on("OrderUpdated", (user, message) => {
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
    await fetch('http://localhost:61242/order')
        .then(x => x.json())
        .then(y => {
            orders = y;
            //console.log(orders);
            display();
        });
}





function display() {
    document.getElementById('resultarea').innerHTML = "";
       orders.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.orderID + "</td><td>"
            + t.carID + "</td><td>"
            + t.howManyDays + "</td><td>" +
            `<button type="button" class="first-button" onclick="remove(${t.orderID})">Delete</button>` +
            //`<div class="spacer"></div>` +
            //`<p></p>` +
            `<button type="button" class="second-button" onclick="showupdate(${t.orderID})">Update</button>`
            + "</td></tr>";
        console.log(orderIdToUpdate);
    });
}

function remove(id) {
    fetch('http://localhost:61242/order/' + id, {
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
    document.getElementById('caridtoupdate').value = orders.find(t => t['orderID'] == id)['carID'];
    document.getElementById('howmanytoupdate').value = orders.find(t => t['orderID'] == id)['howManyDays'];
    document.getElementById('updateformdiv').style.display = 'flex';
    orderIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let carid = document.getElementById('caridtoupdate').value;
    let howmany = document.getElementById('howmanytoupdate').value;
    fetch('http://localhost:61242/order', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { carID: carid, howManyDays: howmany, orderID: orderIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let carid = document.getElementById('carid').value;
    let howmany = document.getElementById('howmany').value;
    let id =
        fetch('http://localhost:61242/order', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify(
                { carID: carid, howManyDays: howmany })
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getdata();
            })
            .catch((error) => { console.error('Error:', error); });
}