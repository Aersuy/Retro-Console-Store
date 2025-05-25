// Real-time logout functionality using SignalR
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/logoutHub")
    .build();

// Start the connection
connection.start().then(function () {
    console.log("SignalR connected for logout monitoring");
    
    // Join a group based on user ID (get from cookie or session)
    var userId = getUserIdFromCookie(); // You'll need to implement this
    if (userId) {
        connection.invoke("JoinUserGroup", userId.toString());
    }
}).catch(function (err) {
    console.error("SignalR connection error: " + err.toString());
});

// Listen for forced logout events
connection.on("ForceLogout", function (reason) {
    // Clear local storage/session storage if you use it
    localStorage.clear();
    sessionStorage.clear();
    
    // Show notification to user
    alert("Your session has been terminated: " + reason);
    
    // Redirect to login page
    window.location.href = "/Auth/Login";
});

// Listen for ban notifications
connection.on("UserBanned", function (banReason, expiryDate) {
    var message = "You have been banned from the system.";
    if (banReason) {
        message += "\nReason: " + banReason;
    }
    if (expiryDate) {
        message += "\nBan expires: " + new Date(expiryDate).toLocaleString();
    } else {
        message += "\nThis is a permanent ban.";
    }
    
    alert(message);
    
    // Clear everything and redirect
    localStorage.clear();
    sessionStorage.clear();
    window.location.href = "/Auth/Login";
});

// Helper function to get user ID from cookie
function getUserIdFromCookie() {
    // You'll need to decode your X-KEY cookie or add a separate userId cookie
    // This is a placeholder - implement based on your cookie structure
    var cookies = document.cookie.split(';');
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i].trim();
        if (cookie.indexOf('userId=') == 0) {
            return cookie.substring(7);
        }
    }
    return null;
}

// Periodic session validation (fallback if SignalR fails)
setInterval(function() {
    fetch('/Auth/ValidateSession', {
        method: 'GET',
        credentials: 'include'
    }).then(function(response) {
        if (response.status === 401) {
            // Session is invalid
            window.location.href = "/Auth/Login";
        }
    }).catch(function(error) {
        console.log('Session validation error:', error);
    });
}, 30000); // Check every 30 seconds 