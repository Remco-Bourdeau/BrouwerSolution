"use strict";
for (const hyperlink of document.getElementsByTagName("a")) {

    if (hyperlink.href.slice(-1) == "#") {

        hyperlink.onclick = zoekBrouwer;
    }
}
async function zoekBrouwer() {
    const response = await
        fetch(`http://localhost:16436/brouwers/${this.dataset.id}`);
    if (response.ok) {
        const brouwer = await response.json();
        document.getElementById("postcode").innerText = brouwer.postcode;
        document.getElementById("gemeente").innerText = brouwer.gemeente;
        document.getElementById("extradata").style.display = "block";
    }
    else if (response.status === 404) {
        alert("Brouwer niet gevonden.");
    } else {
        alert("Technisch probleem, contacteer de helpdesk.");
    }
}