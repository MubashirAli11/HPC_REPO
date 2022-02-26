
import axios from "axios";
import React from "react";

import React, { useEffect, useState } from 'react';
import ReactDOM from 'react-dom';
import ReactPaginate from 'react-paginate';

const baseURL = "http://localhost:63025/api/v1/ship";


const posts = [
    { id: 1, title: 'Post 1' },
    { id: 2, title: 'Post 2' },
    { id: 3, title: 'Post 3' },
    { id: 4, title: 'Post 4' },
    { id: 5, title: 'Post 5' },
    { id: 6, title: 'Post 6' },
    { id: 7, title: 'Post 7' },
    { id: 8, title: 'Post 8' },
    { id: 9, title: 'Post 9' },
    { id: 10, title: 'Post 10' },
    { id: 11, title: 'Post 11' },
    { id: 12, title: 'Post 12' },
  ];


export const Listing = () => {

  

// We start with an empty list of items.
    const [currentItems, setCurrentItems] = useState(null);
    const [pageCount, setPageCount] = useState(0);
// Here we use item offsets; we could also use page offsets
// following the API or data you're working with.
    const [itemOffset, setItemOffset] = useState(0);

    const [currentPage, setCurrentPage] = useState(1);
    const pagePostsLimit = 5;

    const [name, setName] = React.useState('');
    const [code, setCode] = React.useState('');
    const [length, setLength] = React.useState('');
    const [width, setWidth] = React.useState('');
    const [listing, setListing] = React.useState(null);


    React.useEffect(() => {
        axios.get(`${baseURL}`).then((response) => {
            setListing(response.Data);
        });

        const endOffset = itemOffset + itemsPerPage;
        console.log(`Loading items from ${itemOffset} to ${endOffset}`);
        setCurrentItems(listing.slice(itemOffset, endOffset));
        setPageCount(Math.ceil(listing.length / itemsPerPage));
    }, [itemOffset, itemsPerPage]);

    

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