import { Link } from "react-router-dom";
import Listing  from "./listing";
import React, { useState } from "react";

export const Home = () => {


    const [userName, setuserName] = useState('');
    const [password, setpassword] = useState('');


    const login = () => {
        debugger;

        
            const email = 'admin@gmail.com';
            const password = 'Admin@123';
            const data = { email, password };
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(data)
            };
            fetch("http://localhost:8081/api/v1/User", requestOptions)
                .then(response => response.json())
                .then(res => {
                    debugger;
                    localStorage.setItem("token", res.data);
                    window.open("/listing", "_blank");

                   // this.setState({ redirect: "/listing" });
                });
        


       
        
        // fetchWrapper.post(`http://localhost:43061/api/v1/User`, { email, password })
        //     .then(user => {
        //         debugger;
        //         // store user details and jwt token in local storage to keep user logged in between page refreshes
        //         // localStorage.setItem('user', JSON.stringify(user));
        //         // setAuth(user);

        //         // // get return url from location state or default to home page
        //         // const { from } = history.location.state || { from: { pathname: '/' } };
        //         // history.push(from);
        //     });
    };

    return (
        <div className="homeMain">
            <div className="form">
                <form>
                    <div className="inputContainer">
                        <label>Username </label>
                        <input type="text" name="uname" required onChange={e => setuserName(e.target.value)}/>
                    </div>
                    <div className="inputContainer">
                        <label>Password </label>
                        <input type="password" name="pass" required onChange={e => setpassword(e.target.value)} />
                    </div>
                    <div className="button-container">
                        <button className="submitButton buttonClass primaryButton" onClick={() => 
                            login()}>Login</button>
                        {/* <input type="button" className="submitButton buttonClass primaryButton" onClick={() => login()} /> */}
                    </div>
                </form>
            </div>
            {/* <div className="">
                <h1>Home</h1>
                <nav>
                    <Link to="/listing">Listing</Link>
                </nav>
            </div> */}
        </div>
    );
}

export default Home;