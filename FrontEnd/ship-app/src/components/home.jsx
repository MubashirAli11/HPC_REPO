import { Link } from "react-router-dom";

export const Home = () => {
    return (
        <div>
            <h1>Home</h1>
            <nav>
                <Link to="/listing">Listing</Link>
            </nav>
        </div>
    );
}

export default Home;