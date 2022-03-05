
import axios from "axios";
import React, { useState } from "react";
import ReactPaginate from 'react-paginate';
import { useStateWithCallbackLazy } from 'use-state-with-callback';

const baseURL = "http://localhost:8080/api/v1/ship";



export const Listing = () => {


    const [name, setName] = useState('');
    const [code, setCode] = useState('');
    const [length, setLength] = useState('');
    const [width, setWidth] = useState('');
    const [listing, setListing] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPage, setTotalPage] = useState([]);

    const pageSize = 10;


    React.useEffect(() => {
        //LoginApi();

        listingApi();
        
       

    }, []);

    const listingApi = (pageIndex) => {

        const index = typeof pageIndex == "number" ? pageIndex : currentPage;

        axios.get(`${baseURL}?PageIndex=${index}&PageSize=${pageSize}`, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } }).then((response) => {
            const { data, total } = response.data;
            setListing(data);

            setTotalPage(Array.from(Array(Math.ceil(total / pageSize)).keys()));
        });
    };
 

    function handleSubmit(form) {
        console.log(name, code, length, width);
        const request = { name: name, code: code, length: length, width: width };
        axios.post(`${baseURL}`, request, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } })
            .then(res => {
                console.log(res);
                console.log(res.data);

                if(!res.data.isSuccess)
                {
                  alert(res.data.message)
                }
            })
    }

    function edit(id) {
     
        const request = { name: name, code: code, length: length, width: width };

        if(request.length == "")
            request.length = 0;

        if(request.width == "")
            request.width = 0;

        axios.put(`${baseURL}/${id}`, request, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } })
            .then(res => {
                console.log(res);
                console.log(res.data);

                if(!res.data.isSuccess)
                {
                 alert(res.data.message)
                }
                else
                {
                 listingApi();
                }

            })
    }


    function deleteItem(id) {
    
        const request = { };
        axios.delete(`${baseURL}/${id}`, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } }, request)
            .then(res => {
                console.log(res);
                console.log(res.data);
                if(!res.data.isSuccess)
                {
                 alert(res.data.message)
                }
                else
                {
                 listingApi();
                }
                
            })
    }



    const updateCurrentPage = (pageIndex) => {
      
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
                    <h2>Listing View</h2>

                </div>

                <form onSubmit={e => { handleSubmit(e) }}>
                    <div className="inputContainer">
                        <label>Code</label>
                        <input
                            name='code'
                            type='text'
                            required
                            onChange={e => setCode(e.target.value)}
                        />

                    </div>
                    <div className="inputContainer">
                        <label>Name</label>
                        <input
                            name='name'
                            type='text'
                            required
                            onChange={e => setName(e.target.value)}
                        />
                    </div>
                    <div className="inputContainer">
                        <label>Length</label>
                        <input
                            name='length'
                            type='number'
                            required
                            onChange={e => setLength(e.target.value)}
                        />
                    </div>
                    <div className="inputContainer">
                        <label>Width</label>
                        <input
                            name='width'
                            type='number'
                            required
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