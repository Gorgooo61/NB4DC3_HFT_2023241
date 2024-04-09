let brands = [];
let connection = null;

let brandIdToUpdate = -1;

getdata();
setupSignalR();
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:61242/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BrandCreated", (user, message) => {
        getdata();
    });

    connection.on("BrandDeleted", (user, message) => {
        getdata();
    });

    connection.on("BrandUpdated", (user, message) => {
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
    await fetch('http://localhost:61242/brand')
        .then(x => x.json())
        .then(y => {
            brands = y;
            //console.log(brands);
            display();
        });
}





function display() {
    document.getElementById('resultarea').innerHTML = "";
    brands.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.brandID + "</td><td>"
            + t.brandName + "</td><td>"
        + t.brandCountry + "</td><td>" +
        `<button type="button" class="first-button" onclick="remove(${t.brandID})">Delete</button>` +
        //`<div class="spacer"></div>` +
        //`<p></p>` +
        `<button type="button" class="second-button" onclick="showupdate(${t.brandID})">Update</button>`
            + "</td></tr>";
        console.log(brandIdToUpdate);
    });
}

function remove(id) {
    fetch('http://localhost:61242/brand/' + id, {
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
    document.getElementById('brandnametoupdate').value = brands.find(t => t['brandID'] == id)['brandName'];
    document.getElementById('brandcountrytoupdate').value = brands.find(t => t['brandID'] == id)['brandCountry'];
    document.getElementById('updateformdiv').style.display = 'flex';
    brandIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('brandnametoupdate').value;
    let country = document.getElementById('brandcountrytoupdate').value;
    fetch('http://localhost:61242/brand', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { brandName: name, brandCountry: country, brandID: brandIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let name = document.getElementById('brandname').value;
    let country = document.getElementById('brandcountry').value;
    let id = 
    fetch('http://localhost:61242/brand', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { brandName: name, brandCountry: country })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}