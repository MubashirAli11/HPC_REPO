import { Link } from "react-router-dom";

export const Home = () => {

    const login = () => {
        debugger;
        window.open("/listing", "_self");
        
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
                        <input type="text" name="uname" required />
                    </div>
                    <div className="inputContainer">
                        <label>Password </label>
                        <input type="password" name="pass" required />
                    </div>
                    <div className="button-container">
                        <button className="submitButton buttonClass primaryButton" onClick={() => login()}>Login</button>
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