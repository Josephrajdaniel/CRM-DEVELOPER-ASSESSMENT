// Dummy data (Replace with API calls later)
let cases = [
    { id: 1, customerName: "John Doe", query: "Need help with order", channel: "Email", status: "Open" },
    { id: 2, customerName: "Jane Smith", query: "Issue with payment", channel: "WhatsApp", status: "In Progress" }
];

// Function to display cases
function loadCases() {
    const tableBody = document.getElementById("caseTableBody");
    tableBody.innerHTML = ""; // Clear existing rows

    cases.forEach((c, index) => {
        const row = `<tr>
            <td>${c.customerName}</td>
            <td>${c.query}</td>
            <td>${c.channel}</td>
            <td>${c.status}</td>
            <td>
                <button onclick="editCase(${index})">Edit</button>
                <button onclick="deleteCase(${index})">Delete</button>
            </td>
        </tr>`;
        tableBody.innerHTML += row;
    });
}

// Function to add a new case
document.getElementById("caseForm").addEventListener("submit", function(event) {
    event.preventDefault();
    
    let newCase = {
        id: cases.length + 1,
        customerName: document.getElementById("customerName").value,
        query: document.getElementById("queryText").value,
        channel: document.getElementById("caseChannel").value,
        status: "Open"
    };

    cases.push(newCase);
    loadCases();
    this.reset(); // Clear form
});

// Function to delete a case
function deleteCase(index) {
    cases.splice(index, 1);
    loadCases();
}

// Function to edit a case (Simplified)
function editCase(index) {
    let newStatus = prompt("Enter new status (Open, In Progress, Closed):", cases[index].status);
    if (newStatus) {
        cases[index].status = newStatus;
        loadCases();
    }
}

// Function to search and filter cases
function searchCases() {
    let nameFilter = document.getElementById("searchName").value.toLowerCase();
    let channelFilter = document.getElementById("filterChannel").value;

    let filteredCases = cases.filter(c =>
        (c.customerName.toLowerCase().includes(nameFilter)) &&
        (channelFilter === "" || c.channel === channelFilter)
    );

    const tableBody = document.getElementById("caseTableBody");
    tableBody.innerHTML = ""; // Clear existing rows

    filteredCases.forEach((c, index) => {
        const row = `<tr>
            <td>${c.customerName}</td>
            <td>${c.query}</td>
            <td>${c.channel}</td>
            <td>${c.status}</td>
            <td>
                <button onclick="editCase(${index})">Edit</button>
                <button onclick="deleteCase(${index})">Delete</button>
            </td>
        </tr>`;
        tableBody.innerHTML += row;
    });
}

// Load initial data
loadCases();