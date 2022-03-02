
import axios from "axios";
import React, { useState } from "react";
import ReactPaginate from 'react-paginate';
import { useStateWithCallbackLazy } from 'use-state-with-callback';

const baseURL = "http://localhost:63025/api/v1/ship";



export const Listing = () => {


    const [name, setName] = useState('');
    const [token, setToken] = useStateWithCallbackLazy('');
    const [code, setCode] = useState('');
    const [length, setLength] = useState('');
    const [width, setWidth] = useState('');
    const [listing, setListing] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPage, setTotalPage] = useState([]);

    const pageSize = 10;


    React.useEffect(() => {
        LoginApi();

    }, []);

    const listingApi = (pageIndex) => {
        debugger;
        console.log(token)
        const index = typeof pageIndex == "number" ? pageIndex : currentPage;

        axios.get(`${baseURL}?PageIndex=${index}&PageSize=${pageSize}`, { headers: { "Authorization": `Bearer ${token}` } }).then((response) => {
            const { data, Total } = response.data;
            setListing(data);

            setTotalPage(Array.from(Array(Math.ceil(100 / pageSize)).keys()));
        });
    };
    const LoginApi = () => {
        const email = 'admin@gmail.com';
        const password = 'Admin@123';
        const data = { email, password };
        const requestOptions = {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data)
        };
        fetch("http://localhost:43061/api/v1/User", requestOptions)
            .then(response => response.json())
            .then(res => {
                debugger;
                setToken(res.data, () => {
                    listingApi();
                });

            });
    };

    function handleSubmit(form) {
        console.log(name, code, length, width);
        const request = { name: name, code: code, length: length, width: width };
        axios.post(`${baseURL}`, request)
            .then(res => {
                console.log(res);
                console.log(res.data);
            })
    }

    function edit(id) {
        debugger
        const request = { name: name, code: code, length: length, width: width };
        axios.put(`${baseURL}${id}`, request)
            .then(res => {
                console.log(res);
                console.log(res.data);
            })
    }


    function deleteItem(id) {
        debugger
        const request = { name: name, code: code, length: length, width: width };
        axios.delete(`${baseURL}${id}`, request)
            .then(res => {
                console.log(res);
                console.log(res.data);
            })
    }


    function Items({ currentItems }) {
        return (
            <>
                {currentItems &&
                    currentItems.map((item) => (
                        <div>
                            <h3>Item #{item}</h3>
                        </div>
                    ))}
            </>
        );
    }

    const updateCurrentPage = (pageIndex) => {
        debugger
        if (typeof pageIndex == "number") {
            setCurrentPage(pageIndex);
            listingApi(pageIndex);
        }
        else if (pageIndex == "Next" && currentPage < totalPage.length) {
            setCurrentPage(currentPage + 1);
            listingApi(currentPage + 1);
        } else if (pageIndex == "Previous" && currentPage > 1) {
            setCurrentPage(currentPage - 1);
            listingApi(currentPage - 1);
        }
    };



    return (
        <div className="MainContainer">
            <div className="MainContainerForm">
                <div className="topBar">
                    <h2>Listing View{token}</h2>

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
                <table className="MainContainerTableContainer">
                    <thead>
                        <tr>
                            <td>Code</td>
                            <td>Name</td>
                            <td>Length</td>
                            <td>Width</td>
                        </tr>
                    </thead>
                    <tbody>
                        {listing && listing.map((item) =>
                            <tr key={item.id}>
                                <td>{item.code}</td>
                                <td>{item.name}</td>
                                <td>{item.length}</td>
                                <td>{item.width}</td>
                                <td className="actionTableElement">
                                    <div className="actionBlock">
                                        <button className="buttonClass secondaryButton" onClick={() => deleteItem(item.id)}>Delete</button>
                                        <button className="buttonClass primaryButton" onClick={() => edit(item.id)}>Edit</button>

                                    </div>
                                </td>
                            </tr>
                        )}

                    </tbody>

                </table>
                <div className="MainContainerTablePagination">
                    <div onClick={() => updateCurrentPage("Previous")}>pre</div>
                    {totalPage && totalPage.map((item) => <div key={item} onClick={() => updateCurrentPage(item + 1)}>{item + 1}</div>)}
                    <div onClick={() => updateCurrentPage("Next")}>next</div>
                </div>
            </div>
            <div>

            </div>
        </div>
    );

}

export default Listing;