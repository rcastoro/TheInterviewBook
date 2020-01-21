
    var InterviewBook = [];
    var Users = [];
    var NHLTeams = [];

    $(async function()  {
            await fetchData();
            loadUsers();
            insertNHLOptions();
            console.log(Users);
    });
    
    async function fetchData()
    {
        //Get main engine (version, theme, etc..)
        await fetch('https://de4a52c6.ngrok.io/api/InterviewBook')
        .then(res=>res.json())
        .then((books) => {
            InterviewBook = books;
        })
        .catch(console.log)

        //Get users
        await fetch('https://de4a52c6.ngrok.io/api/InterviewBookUser')
        .then(res=>res.json())
        .then((users) => {
            Users = users;
        })
        .catch(console.log)

        //Get NHL Teams
        await fetch('https://de4a52c6.ngrok.io/api/InterviewBook/nhl')
        .then(res=>res.json())
        .then((nhlteams) => {
            NHLTeams = nhlteams;
        })
        .catch(console.log)


    }

    function loadUsers()
    {

        // We need to assign a numbered ID to each row so we can append chilren properly
        var rowInc = 0;

        $.each(Users, function(i) {
            
            //add row end
            if(i%3==0)
            {
                $('#row'+rowInc).append("</div>");
            }

            //add row start
            if(i==0 || i%3==0)
            {
                rowInc++;
                $('#users').append("<div class='row' id='row"+rowInc+"'>");
            }

            //Construct our user cards
            var userCard = 
                "<div class='col-md-4 mb-5'>"+
                    "<div class='card h-100'>"+
                        "<div class='card-body'>"
                            +"<h2 class='card-title'>"+Users[i].firstName + " " + Users[i].lastName +"</h2>"
                            +"<p class='card-text'>DOB: "+Users[i].dob+"</p>"
                            +"<p class='card-text'>Email: "+Users[i].email+"</p>"
                            +"<p class='card-text'>Phone Number: "+Users[i].phoneNumber+"</p>"
                            +"<p class='card-text'>Prefers: "+Users[i].pepsiOrCoke+"</p>"
                            +"<p class='card-text'>Fav Hockey Team: "+Users[i].hockeyTeamID+"</p>"
                        +"</div>"
                        +"<div class='card-footer'>"
                            +"<a href='#' class='btn btn-primary btn-sm'>More Info</a>"
                        +"</div>"
                    +"</div>"
                +"</div>"
            
            // add card / user
            $('#row'+rowInc).append(userCard);

            //add row end
            if(i==Users.length)
            {
                $('#row'+rowInc).append("</div>");
            }
        })
    }

    function insertNHLOptions()
    {
        $.each(NHLTeams.teams, function(i) {
            var nhlTeamOption = 
                "<option key="+NHLTeams.teams[i].name+">"+NHLTeams.teams[i].name+"</option>"
            $('#HockeyTeamID').append($("<option></option>")
                                .attr("value", NHLTeams.teams[i].name)
                                .text(NHLTeams.teams[i].name)
                                )
        })
    }

    const formArray = form => JSON.stringify(
        Array.from(new FormData(form).entries())
             .reduce((m, [ key, value ]) => Object.assign(m, { [key]: value }), {})
      );

    $("#form_signup").on("submit", async function (event) {
    
        event.preventDefault();

        // Clear previous form validation
        clearFormValidation();

        const json = formArray(this);
        console.log(json);

        if(validateForm())
        {
            await fetch('https://de4a52c6.ngrok.io/api/InterviewBookUser', {
                method: 'POST',
                body: json,
                headers: {
                    // 'Content-Type': 'application/json'
                    'Content-Type': 'application/json',
                }
            })

            location.reload(true);
        }
    })

    function validateForm()
    {
        let firstNameValid = true;
        let lastNameValid = true;
        let emailValid = true;
        let phoneNumberValid = true;
        let DOBValid = true;

        if($("#FirstName").val() == "")
        {
            firstNameValid = false;
            $("#dFirstName").append("<div class='p-3 mb-2 bg-warning text-dark errorValidation'>Enter a first name</div>")
        }

        if($("#LastName").val() == "")
        {
            lastNameValid = false;
            $("#dLastName").append("<div class='p-3 mb-2 bg-warning text-dark errorValidation'>Enter a last name</div>")
        }
        
        // Email?
        if($("#Email").val() == "")
        {
            emailValid = false;
            $("#dEmail").append("<div class='p-3 mb-2 bg-warning text-dark errorValidation'>Enter an email</div>")
        }

        // Valid email?
        var email = $("#Email").val().toString();
        console.log(email);
        console.log(email.indexOf("."));
        if(email.indexOf(".") <0 || email.indexOf("@")<0 )
        {
            emailValid = false;
            $("#dEmail").append("<div class='p-3 mb-2 bg-warning text-dark errorValidation'>Enter a valid email</div>")
        }

        if($("#PhoneNumber").val() == "")
        {
            phoneNumberValid = false;
            $("#dPhoneNumber").append("<div class='p-3 mb-2 bg-warning text-dark errorValidation' >Enter a phone number</div>")
        }
        else
        {
            var phoneNumber = $("#PhoneNumber").val().toString();
            if(!phoneNumber.match(/^\d+$/) || phoneNumber.length < 10)
            {
                phoneNumberValid = false;
                $("#dPhoneNumber").append("<div class='p-3 mb-2 bg-warning text-dark errorValidation' >Enter a valid phone number</div>") ;
            }
        }

        if($("#DOB").val() == "")
        {
            DOBValid = false;
            $("#dDOB").append("<div class='p-3 mb-2 bg-warning text-dark errorValidation'>Enter a date of birth</div>");
        }
        else
        {
            var dob = $("#DOB").val().toString();
            var regEx = /^\d{2}\/\d{2}\/\d{4}$/;
            if(!dob.match(regEx))
            {
                DOBValid = false;
                $("#dDOB").append("<div class='p-3 mb-2 bg-warning text-dark errorValidation'>Enter a valid date of birth (DD/MM/YYYY</div>")
            }
        }

        return firstNameValid && lastNameValid && emailValid && phoneNumberValid && DOBValid
    }

    function clearFormValidation()
    {
        console.log("remove");
        $(".errorValidation").remove();
    }