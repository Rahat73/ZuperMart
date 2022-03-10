// ------------------------------------
// index page---------------
const logindropdown = document.getElementById("LoggedIn");
const logoutdropdown = document.getElementById("LoggedOut");
function lgdd(li) {
    if (li == 0) {
        logindropdown.style.display = "none";
        logoutdropdown.style.display = "block";
    }
    else {
        logoutdropdown.style.display = "none";
        logindropdown.style.display = "block";
    }
}
// login page--------------------
const indicator = document.querySelector(".indicator");
const input = document.querySelector("#UserPassword");
var inputc = document.querySelector("#UserCPassword");
const weak = document.querySelector(".weak");
const medium = document.querySelector(".medium");
const strong = document.querySelector(".strong");
const text = document.querySelector(".text");
const textc = document.querySelector(".textc");
const textop=document.querySelector(".textop");
inputc.disabled=true;
const regbtn = document.getElementById("reg");
let regExpWeak = /[a-z]/;
let regExpMedium = /\d+/;
let regExpStrong = /.[!,@,#,$,%,^,&,*,?,_,-,(,)]/;

function trigger() {
    if (input.value != "") {
        indicator.style.display = "block";
        indicator.style.display = "flex";
        if (input.value.length <= 3 && (input.value.match(regExpWeak) || (input.value.match(regExpMedium) || input.value.match(regExpStrong)))) no = 1;
        if (input.value.length >= 6 && ((input.value.match(regExpWeak) && input.value.match(regExpMedium)) || (input.value.match(regExpMedium) && input.value.match(regExpStrong)) || (input.value.match(regExpWeak) && input.value.match(regExpStrong)))) no = 2;
        if (input.value.length >= 6 && input.value.match(regExpWeak) && input.value.match(regExpMedium) && input.value.match(regExpStrong)) no = 3;
        if (no == 1) {
            weak.classList.add("active");
            text.style.display = "block";
            text.textContent = "Your password is too weak!";
            text.classList.add("weak");
            regbtn.disabled = true;
        }
        if (no == 2) {
            medium.classList.add("active");
            text.textContent = "Your password is medium";
            text.classList.add("medium");
            regbtn.disabled = true;
        } else {
            medium.classList.remove("active");
            text.classList.remove("medium");
            regbtn.disabled = true;
        }
        if (no == 3) {
            inputc.disabled=false;
            medium.classList.add("active");
            strong.classList.add("active");
            text.textContent = "Your password is strong";
            text.classList.add("strong");
            regbtn.disabled = true;
        } else {
            inputc.disabled=true;
            strong.classList.remove("active");
            text.classList.remove("strong");
            regbtn.disabled = true;
        }
    } else {
        indicator.style.display = "none";
        text.style.display = "none";
        regbtn.disabled = false;
    }
}
function indicoff() {
    indicator.style.display = "none";
    text.style.display = "none";
    textc.style.display = "none";
    regbtn.disabled = false;
}
function checkPass() {
    if (inputc.value != "") {
        if (inputc.value != input.value) {
            regbtn.disabled = true;           
            textc.style.display="block";
            textc.textContent = "Password Not Matched";
            textc.classList.add("weak");
        }
        if (inputc.value == input.value) {
            regbtn.disabled = false;
            textc.textContent = "Password Matched";
            textc.classList.add("strong");
        }    
    } else {
        textc.style.display = "none";
        regbtn.disabled = true;
    }

}
var oldp=document.querySelector("#OldPassword");
const newp=document.getElementById("UserPassword");
function checkop(oldpassword){
    if(oldp.value!=""){
        if(oldp.value!=oldpassword.value){
            newp.disabled=true;
            textop.style.display="block";
            textop.textContent="Password Not Matched";
            textop.classList.remove("strong");
            textop.classList.add("weak");
        }
        if(oldp.value==oldpassword.value){
            textop.textContent="Password Matched";
            textop.classList.add("strong");
            newp.disabled=false;
        }

    }
    else{
        textop.style.display="none";
        newp.disabled=true;
    }

}

//  ---------------------------------
const userImg = document.querySelector('.userImage');
const userPho = document.querySelector('#userphoto');
const uploadImg = document.querySelector('#uploadImage');
const uploadBtn = document.querySelector('#uploadbtn');
var profilename = document.getElementById("prousername");
var profileemail = document.getElementById("proemail");
var profilephone = document.getElementById("prophoneNumber");
var editpro = document.getElementById("EditProfile");
var savepro = document.getElementById("SaveProfile");
const reader = new FileReader();
editpro.addEventListener("click", editProfile);
function editProfile() {
    window.location.href = "user-profile.php?edit=profile";
    return true;
}
function profile(i) {
    if (i == 0) {
        uploadBtn.style.display = "none";
        profilename.disabled = true;
        profileemail.disabled = true;
        profilephone.disabled = true;
        editpro.style.display = "block";
        savepro.style.display = "none";
    } else {
        profilename.disabled = false;
        profileemail.disabled = false;
        profilephone.disabled = false;
        editpro.style.display = "none";
        savepro.style.display = "block";
        userImg.addEventListener("mouseover", function () {
            uploadBtn.style.display = "block";

        });
        userImg.addEventListener("mouseout", function () {
            uploadBtn.style.display = "none";
        });
        uploadImg.addEventListener("change", function () {
            const choosedFile = this.files[0];
            if (choosedFile) {
                reader.addEventListener("load", function () {
                    userPho.setAttribute("src", reader.result);
                });
                reader.readAsDataURL(choosedFile);
            }
        });


    }
}
console.log(userImg);
