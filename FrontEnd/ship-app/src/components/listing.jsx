
import axios from "axios";
import React from "react";

const baseURL = "https://jsonplaceholder.typicode.com/posts";


export const Listing = () => {
    const [name, setName] = React.useState('');
    const [code, setCode] = React.useState('');
    const [length, setLength] = React.useState('');
    const [width, setWidth] = React.useState('');
    const [listing, setListing] = React.useState(null);


    React.useEffect(() => {
        axios.get(`${baseURL}`).then((response) => {
            setListing(response.data);
        });
    }, []);

    

    function handleSubmit(form) {
        console.log(name, code, length, width);
        const request = { name: name, code: code, length: length, width: width };
        axios.post(`https://jsonplaceholder.typicode.com/users`, request)
            .then(res => {
                console.log(res);
                console.log(res.data);
            })
    }

    function edit(id) {
        debugger
        const request = { name: name, code: code, length: length, width: width };
        axios.put(`https://jsonplaceholder.typicode.com/users/${id}`, request)
            .then(res => {
                console.log(res);
                console.log(res.data);
            })
    }


    function deleteItem(id) {
        debugger
        const request = { name: name, code: code, length: length, width: width };
        axios.post(`https://jsonplaceholder.typicode.com/users`, request)
            .then(res => {
                console.log(res);
                console.log(res.data);
            })
    }

    return (
        <div className="MainContainer">
            <div className="MainContainerForm">
                <div className="topBar">
                    <h2>Listing View</h2>

                </div>

                <form onSubmit={e => { handleSubmit(e) }}>
                    <div className="inputContainer">
                        <label>Code</label>
                        <input
                            name='code'
                            type='text'
                            onChange={e => setCode(e.target.value)}
                        />

                    </div>
                    <div className="inputContainer">
                        <label>Name</label>
                        <input
                            name='name'
                            type='text'
                            onChange={e => setName(e.target.value)}
                        />
                    </div>
                    <div className="inputContainer">
                        <label>Length</label>
                        <input
                            name='length'
                            type='number'
                            onChange={e => setLength(e.target.value)}
                        />
                    </div>
                    <div className="inputContainer">
                        <label>Width</label>
                        <input
                            name='width'
                            type='number'
                            onChange={e => setWidth(e.target.value)}
                        />
                    </div>
                    <div className="inputContainer">
                        <input
                            className='submitButton buttonClass primaryButton'
                            type='submit'
                            value='Create'
                        />
                    </div>
                </form>
            </div>

            <div className="MainContainerTable">
                <table>
                    <thead>
                        <tr>
                            <td>Body</td>
                            <td>Title</td>
                            <td>Action</td>
                        </tr>
                    </thead>
                    <tbody>
                        {listing && listing.map((item) =>
                            <tr key={item.id}>
                                <td>{item.body}</td>
                                <td>{item.title}</td>
                                <td>
                                    <div className="actionBlock">
                                        <button className="buttonClass secondaryButton" onClick={() => deleteItem(item.id)}>Delete</button>
                                        <button className="buttonClass primaryButton" onClick={() => edit(item.id)}>Edit</button>

                                    </div>
                                </td>
                            </tr>
                        )}
                    </tbody>

                </table>
            </div>
        </div>
    );
}

export default Listing;