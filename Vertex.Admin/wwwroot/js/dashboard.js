const apiBase = "https://localhost:7280/api";
//const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYXJ0ZW5pciIsIkN1c3RvbWVySWQiOiI0IiwiZXhwIjoxNzc1MzM4NDk2LCJpc3MiOiJ2ZXJ0ZXgtYXBpIiwiYXVkIjoidmVydGV4LWNsaWVudCJ9.rORABF5JXZhLU-6Wz3kJ8PIgmUkHxNknxyjIR0Htbb8";

// =======================
// 🔹 DASHBOARD DATA
// =======================
async function loadDashboard() {
    try {
        const response = await fetch(`${apiBase}/dashboard`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        const data = await response.json();

        document.getElementById("totalCustomers").innerText = data.totalCustomers;
        document.getElementById("totalStations").innerText = data.totalStations;
        document.getElementById("stationsInUse").innerText = data.stationsInUse;
        document.getElementById("activeSessions").innerText = data.activeSessions;
        document.getElementById("todayRevenue").innerText = data.todayRevenue.toFixed(2);
    } catch (error) {
        console.error("Erro ao carregar dashboard:", error);
    }
}

// =======================
// 🔹 STATIONS
// =======================
async function loadStations() {
    try {
        const response = await fetch(`${apiBase}/stations`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        const stations = await response.json();

        const table = document.getElementById("stationsTable");
        table.innerHTML = "";

        stations.forEach(s => {
            const isInUse = s.status === 1;

            const row = `
                <tr>
                    <td>${s.name}</td>
                    <td style="color:${isInUse ? 'red' : 'green'}">
                        ${isInUse ? 'In Use' : 'Available'}
                    </td>
                </tr>
            `;

            table.innerHTML += row;
        });
    } catch (error) {
        console.error("Erro ao carregar stations:", error);
    }
}

// =======================
// 🔹 SIGNALR CONNECTION
// =======================
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7280/hubs/dashboard")
    .withAutomaticReconnect()
    .build();

// 🔥 Evento: sessão iniciada
connection.on("SessionStarted", data => {
    console.log("SessionStarted:", data);
    loadDashboard();
    loadStations();
});

// 🔥 Evento: sessão encerrada
connection.on("SessionEnded", data => {
    console.log("SessionEnded:", data);
    loadDashboard();
    loadStations();
});

// =======================
// 🔹 START CONNECTION
// =======================
async function startSignalR() {
    try {
        await connection.start();
        console.log("SignalR conectado");
    } catch (err) {
        console.error("Erro ao conectar SignalR:", err);
        setTimeout(startSignalR, 5000);
    }
}

// =======================
// 🔹 INIT
// =======================
loadDashboard();
loadStations();
startSignalR();