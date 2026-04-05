const apiBase = "https://localhost:7280/api";

// DASHBOARD DATA
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


// STATIONS
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

            const statusBadge = isInUse
                ? '<span class="badge bg-danger">In Use</span>'
                : '<span class="badge bg-success">Available</span>';

            const row = `
                <tr>
                    <td>${s.name}</td>
                    <td>${statusBadge}</td>
                </tr>
            `;

            table.innerHTML += row;
        });
    } catch (error) {
        console.error("Erro ao carregar stations:", error);
    }
}


// CHART
let chart;

async function loadRevenueChart() {
    try {
        const response = await fetch(`${apiBase}/dashboard/revenue-chart?days=7`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        const data = await response.json();

        const labels = data.map(x => new Date(x.date).toLocaleDateString());
        const revenue = data.map(x => x.revenue);
        const sessions = data.map(x => x.sessions);

        const ctx = document.getElementById("revenueChart").getContext("2d");

        if (chart) {
            chart.destroy();
        }

        chart = new Chart(ctx, {
            type: "line",
            data: {
                labels: labels,
                datasets: [
                    {
                        label: "Revenue",
                        data: revenue,
                        borderWidth: 2
                    },
                    {
                        label: "Sessions",
                        data: sessions,
                        borderWidth: 2
                    }
                ]
            }
        });

    } catch (error) {
        console.error("Erro ao carregar gráfico:", error);
    }
}


//DARK MODE
function toggleDarkMode() {
    document.body.classList.toggle("dark-mode");

    const isDark = document.body.classList.contains("dark-mode");

    localStorage.setItem("darkMode", isDark);
}
function loadTheme() {
    const isDark = localStorage.getItem("darkMode") === "true";

    if (isDark) {
        document.body.classList.add("dark-mode");
    }
}


// SIGNALR CONNECTION
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7280/hubs/dashboard")
    .withAutomaticReconnect()
    .build();

// Evento: sessão iniciada
connection.on("SessionStarted", data => {
    console.log("SessionStarted:", data);
    loadDashboard();
    loadStations();
    loadRevenueChart()
});

// Evento: sessão encerrada
connection.on("SessionEnded", data => {
    console.log("SessionEnded:", data);
    loadDashboard();
    loadStations();
    loadRevenueChart()
});


// START CONNECTION
async function startSignalR() {
    try {
        await connection.start();
        console.log("SignalR conectado");
    } catch (err) {
        console.error("Erro ao conectar SignalR:", err);
        setTimeout(startSignalR, 5000);
    }
}

loadDashboard();
loadStations();
loadRevenueChart()
loadTheme();
startSignalR();