// form inputs
let userInputField = document.getElementById('userSearchInput');
let userSubmitBtn = document.getElementById('userSearchSubmitInput');
let clearUserInfoBtn = document.getElementById('clearUserResults');
let userInfoPanel = document.getElementById('userInfoPanel');

// event handlers
userSubmitBtn.addEventListener("click", (event) => { handleUserSubmit(event); });
clearUserInfoBtn.addEventListener("click", (event) => { handleClearResultsClick(); });

handleClearResultsClick = () => {
    console.log()
    userInfoPanel.innerHTML = '';
}

handleUserSubmit = (event) =>
{
    event.preventDefault();
    getUser();  
}

getUserInput = () => {
    return userInputField.value.trim();
}

getUser = () => {
    let userId = getUserInput();
    let targetUri = `User/Get?userId=${userId}`;
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState == XMLHttpRequest.DONE) {
            handleGetUserResponse(xhr.response)
        }
    }
    xhr.open('GET', targetUri, true);
    xhr.send(null);
}

handleGetUserResponse = (wargamingUser) => {
    if (wargamingUser != null) {
        let parsedUser = JSON.parse(wargamingUser);
        let userEntryDiv = document.createElement("div");
        let usernameDiv = document.createElement("div");
        let accountIdDiv = document.createElement("div");
        let statusBtn = document.createElement("button");

        // set class for user wrapper div
        userEntryDiv.classList.add("user-info-entry");
        userEntryDiv.setAttribute("id", parsedUser.accountId);

        // create nickname div
        usernameDiv.textContent = `Nickname: ${parsedUser.nickname}`;
        userEntryDiv.append(usernameDiv);

        // create account number div
        accountIdDiv.textContent = `Account: ${parsedUser.accountId}`;
        userEntryDiv.append(accountIdDiv);

        // create status button
        statusBtn.textContent = 'Get Stats for User';
        statusBtn.addEventListener("click", () => { /*handleUserInfoClick(parsedUser.accountId);*/ });
        userEntryDiv.append(statusBtn);

        userInfoPanel.append(userEntryDiv);
    }
}