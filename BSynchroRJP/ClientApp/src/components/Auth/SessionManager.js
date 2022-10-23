const SessionManager = {

    getToken() {
        debugger
        const token = sessionStorage.getItem('token');
        if (token) return token;
        else return null;
    },
    getUserId() {
        const userId = sessionStorage.getItem('userId');
        if(userId) return userId;
        else return null;
    },

    setUserSession(userName, token, userId, usersRole) {
        sessionStorage.setItem('userName', userName);
        sessionStorage.setItem('token', token);
        sessionStorage.setItem('userId', userId);
        sessionStorage.setItem('usersRole', usersRole);
    },

    removeUserSession(){
        sessionStorage.removeItem('userName');
        sessionStorage.removeItem('token');
        sessionStorage.removeItem('userId');
        sessionStorage.removeItem('usersRole');
    }
}

export default SessionManager;