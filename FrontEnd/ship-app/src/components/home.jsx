import { Link } from "react-router-dom";
import Listing  from "./listing";
import React, { useState } from "react";

export const Home = () => {


    const [email, setemail] = useState('');
    const [password, setpassword] = useState('');

    const login = () => {
   

            const data = { email, password };
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(data)
            };
            fetch("http://localhost:8081/api/v1/User", requestOptions)
                .then(response => response.json())
                .then(res => {
       

                    if(!res.isSuccess)
                        alert(res.message)
                    else
                    {

                        localStorage.setItem("token", res.data);
                        window.open("/listing", "_self");
                    }
                });
        



    };



    function handleSubmit(event) {
        event.preventDefault();
      }


    return (
        <div className="homeMain">
            <div className="form">
                <form onSubmit={handleSubmit}>
                    <div className="inputContainer">
                        <label>Username </label>
                        <input type="text" name="uname" required onChange={e => setemail(e.target.value)}/>
                    </div>
                    <div className="inputContainer">
                        <label>Password </label>
                        <input type="password" name="pass" required onChange={e => setpassword(e.target.value)} />
                    </div>
                    <div className="button-container">
                        <button className="submitButton buttonClass primaryButton" onClick={() => 
                            login()}   >Login</button>
                        {/* <input type="button" className="submitButton buttonClass primaryButton" onClick={() => login()} /> */}
                    </div>

           
                </form>
            </div>



        </div>
    );
}

export default Home;