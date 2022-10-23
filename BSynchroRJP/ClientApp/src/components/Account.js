import React, { useState, useEffect, useMemo, useRef } from "react";
import { useTable } from "react-table";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import Select from 'react-dropdown-select';
import { postData, getData, deleteData, putData } from "../services/AccessAPI";
import SessionManager from './Auth/SessionManager'

const Account = (props) => {
    const userId = SessionManager.getUserId();
    const initialEmployeeState = {
        id: null,
        accountStatusId: null,
        currency: "",
        AccountTypeId: null,
        AccountSubTypeId: null,
        CustomerId: null,
        TotalBalance: 0
    };
    const [accounts, setAccounts] = useState([]);
    const [searchName, setSearchName] = useState("");
    const [isOpened, setIsOpened] = useState(false);
    const [account, setAccount] = useState(initialEmployeeState);
    const [accountStatuses, setAccountStatuses] = useState([]);
    const [selectedAccountStatus, setSelectedAccountStatus] = useState([{ value: '', label: '' }]);
    const [accountTypes, setAccountTypes] = useState([]);
    const [selectedAccountType, setSelectedAccountType] = useState([{ value: '', label: '' }]);
    const [accountSubTypes, setAccountSubTypes] = useState([]);
    const [selectedAccountSubType, setSelectedAccountSubType] = useState([{ value: '', label: '' }]);
    const [isAdd, setIsAdd] = useState(false);
    const [accountStatusValidationMessage, setAccountStatusValidationMessage] = useState("");
    const [accountSubTypeValidationMessage, setAccountSubTypeValidationMessage] = useState("");
    const [accountTypeValidationMessage, setAccountTypeValidationMessage] = useState("");
    const accountsRef = useRef();
    var accountStatusesTemp = [{ value: '', label: '' }];
    var accountTypesTemp = [{ value: '', label: '' }];
    var accountSubTypesTemp = [{ value: '', label: '' }];
    accountsRef.current = accounts;

    useEffect(() => {
        retrieveAccounts();
    }, []);

    const onChangeSearchName = (e) => {
        const searchName = e.target.value;
        setSearchName(searchName);
    };

    const retrieveAccounts = () => {
        getData('api/customers/' + userId + '/accounts')
            .then((response) => {
                if(response && response.length > 0) {
                    setAccounts(response.data);
                } 
            })
            .catch((e) => {
                toast.warning(e, {
                    position: toast.POSITION.TOP_RIGHT
                });
            });
    };
    const retrieveAccountsStatuses = () => {
        getData('api/accountstatuses')
            .then((response) => {
                debugger
                if(response && response.length > 0) {
                    accountStatusesTemp = [{ value: 0, label: '' }];
                    response.forEach(data => accountStatusesTemp.push({ value: data.id, label: data.name }));
                    setAccountStatuses(accountStatusesTemp);
                }
            })
            .catch((e) => {
                toast.warning(e, {
                    position: toast.POSITION.TOP_RIGHT
                });
            });
    };
    const retrieveAccountsTypes = () => {
        getData('api/accounttypes')
            .then((response) => {
                debugger
                if(response && response.length > 0) {
                    accountTypesTemp = [{ value: 0, label: '' }];
                    response.forEach(data => accountTypesTemp.push({ value: data.id, label: data.type }));
                    setAccountTypes(accountTypesTemp);
                }
            })
            .catch((e) => {
                toast.warning(e, {
                    position: toast.POSITION.TOP_RIGHT
                });
            });
    };
    const retrieveAccountSubTypes = () => {
        getData('api/accountsubtypes')
            .then((response) => {
                debugger
                if(response && response.length > 0) {
                    accountSubTypesTemp = [{ value: 0, label: '' }];
                    response.forEach(data => accountSubTypesTemp.push({ value: data.id, label: data.subType }));
                    setAccountSubTypes(accountSubTypesTemp);
                }
            })
            .catch((e) => {
                toast.warning(e, {
                    position: toast.POSITION.TOP_RIGHT
                });
            });
    };
    const addAccount = () => {
        setAccount({ id: null, accountStatusId: null, currency: "", AccountTypeId: null, AccountSubTypeId: null, CustomerId: null, TotalBalance: 0 });
        retrieveAccountsStatuses();
        retrieveAccountsTypes();
        retrieveAccountSubTypes();
        setAccountStatuses([{ value: 0, label: '' }]);
        setAccountTypes([{ value: 0, label: '' }]);
        setAccountSubTypes([{ value: 0, label: '' }]);
        setAccountStatusValidationMessage("");
        setAccountSubTypeValidationMessage("");
        setAccountTypeValidationMessage("");
        setIsAdd(true);
        setIsOpened(true);
        //props.history.push("/add");
    }
    const openAccount = (rowIndex) => {
        const id = accountsRef.current[rowIndex].id;
        getAccount(id);
        setAccountStatusValidationMessage("");
        setAccountSubTypeValidationMessage("");
        setAccountTypeValidationMessage("");
        setIsAdd(false);
        setIsOpened(true);
        //props.history.push("/employees/" + id);
    };
    const getAccount = id => {
        getData('api/customers' + userId + '/accounts/' + id)
            .then(response => {
                setAccount(response.data);
                debugger

            })
            .catch(e => {
                toast.warning(e, {
                    position: toast.POSITION.TOP_RIGHT
                });
            });
    };
    const deleteAccount = (rowIndex) => {
        const id = accountsRef.current[rowIndex].id;

        deleteData(id)
            .then((response) => {
                props.history.push("/account");

                let newAccounts = [...accountsRef.current];
                newAccounts.splice(rowIndex, 1);

                setAccounts(newAccounts);
                toast.success('Account wa deleted successfuly', {
                    position: toast.POSITION.TOP_RIGHT
                });
            })
            .catch((e) => {
                toast.warning(e, {
                    position: toast.POSITION.TOP_RIGHT
                });
            });
    };
    const handleInputChange = event => {
        const { name, value } = event.target;
        setAccount({ ...account, [name]: value });
    };
    const saveAccount = () => {
        //if(validateForm() === false) {
            var data = {
                accountStatusId: selectedAccountStatus[selectedAccountStatus.length - 1].value,
                currency: account.currency,
                AccountTypeId: selectedAccountType[selectedAccountType.length - 1].value,
                AccountSubTypeId: selectedAccountSubType[selectedAccountSubType.length - 1].value,
                CustomerId: null,
                TotalBalance: account.TotalBalance
            };
    
            postData("api/accounts",data)
                .then(response => {
                    setAccount({
                        id: response.data.id,
                        accountStatusId: response.data.accountStatusId,
                        currency: response.data.currency,
                        AccountTypeId: response.data.AccountTypeId,
                        AccountSubTypeId: response.data.AccountSubTypeId,
                        CustomerId: response.data.CustomerId,
                        TotalBalance: response.data.TotalBalance
                    });
                    retrieveAccounts();
                    toast.success('Employee was created successfuly', {
                        position: toast.POSITION.TOP_RIGHT
                    });
                    setIsOpened(false);
                })
                .catch(e => {
                    toast.warning(e, {
                        position: toast.POSITION.TOP_RIGHT
                    });
                });
        //}
    };
    const updateAccount = () => {
        //if(validateForm() === false) {
            account.accountStatusId = selectedAccountStatus[selectedAccountStatus.length - 1].value;
            account.AccountTypeId = selectedAccountType[selectedAccountType.length - 1].value;
            account.AccountSubTypeId = selectedAccountSubType[selectedAccountSubType.length - 1].value;
            putData("api/accounts/" + account.id, account)
            .then(response => {
                retrieveAccounts();
                toast.success('Account was updated successfully', {
                    position: toast.POSITION.TOP_RIGHT
                });
                setIsOpened(false);
            })
            .catch(e => {
                toast.warning(e, {
                    position: toast.POSITION.TOP_RIGHT
                });
            });
        //}
    };
    //const validateForm = () => {
    //   
    //}
    const columns = useMemo(
        () => [
            {
                Header: "Account Status",
                accessor: "accountStatus.name",
            },
            {
                Header: "Account SubType",
                accessor: "accountSubType.subType",
            },
            {
                Header: "Account Type",
                accessor: "accountType.type",
            },
            {
                Header: "Actions",
                accessor: "actions",
                Cell: (props) => {
                    const rowIdx = props.row.id;
                    return (
                        <div>
                            <span onClick={() => openAccount(rowIdx)}>
                                <i className="far fa-edit action mr-2"></i>
                            </span>

                            <span onClick={() => deleteAccount(rowIdx)}>
                                <i className="fas fa-trash action"></i>
                            </span>
                        </div>
                    );
                },
            },
        ],
        []
    );

    const {
        getTableProps,
        getTableBodyProps,
        headerGroups,
        rows,
        prepareRow,
    } = useTable({
        columns,
        data: accounts,
    });

    return (
        <>
            <div className="list row">
                <div className="col-md-8">
                    <div className="input-group mb-3">
                        <input
                            type="text"
                            className="form-control"
                            placeholder="Search by Name"
                            value={searchName}
                            onChange={onChangeSearchName}
                        />
                    </div>
                </div>
                <div className="col-md-2"></div>
                <div className="col-md-2">
                    <Button
                        variant="primary"
                        onClick={addAccount}
                        style={{ float: "right" }}
                    >
                        Add Account
                    </Button>
                </div>
                <div className="col-md-12 list">
                    <table
                        className="table table-striped table-bordered"
                        {...getTableProps()}
                    >
                        <thead>
                            {headerGroups.map((headerGroup) => (
                                <tr {...headerGroup.getHeaderGroupProps()}>
                                    {headerGroup.headers.map((column) => (
                                        <th {...column.getHeaderProps()}>
                                            {column.render("Header")}
                                        </th>
                                    ))}
                                </tr>
                            ))}
                        </thead>
                        <tbody {...getTableBodyProps()}>
                            {rows.map((row, i) => {
                                prepareRow(row);
                                return (
                                    <tr {...row.getRowProps()}>
                                        {row.cells.map((cell) => {
                                            return (
                                                <td {...cell.getCellProps()}>{cell.render("Cell")}</td>
                                            );
                                        })}
                                    </tr>
                                );
                            })}
                        </tbody>
                    </table>
                </div>

            </div>
            <ToastContainer />
            <Modal show={isOpened} onHide={() => setIsOpened(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Modal heading</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className="submit-form">
                        <div>
                            <div className="form-group">
                                <label htmlFor="accountStatus">Account Status</label>
                                <Select
                                    options={accountStatusesTemp}
                                    searchable={true}
                                    multi={false}
                                    values={selectedAccountStatus}
                                    onChange={(value) => setSelectedAccountStatus(value)}
                                />
                                <div className="text-danger">{ accountStatusValidationMessage }</div>
                            </div>

                            <div className="form-group">
                                <label htmlFor="accountSubTypes">Account SubType</label>
                                <Select
                                    options={accountSubTypesTemp}
                                    searchable={true}
                                    multi={false}
                                    values={selectedAccountSubType}
                                    onChange={(value) => setSelectedAccountSubType(value)}
                                />
                                <div className="text-danger">{ accountSubTypeValidationMessage }</div>
                            </div>
                            <div className="form-group">
                                <label htmlFor="accountTypes">Account Type</label>
                                <Select
                                    options={accountTypesTemp}
                                    searchable={true}
                                    multi={false}
                                    values={selectedAccountType}
                                    onChange={(value) => setSelectedAccountType(value)}
                                />
                                <div className="text-danger">{ accountTypeValidationMessage }</div>
                            </div>
                        </div>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setIsOpened(false)}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={isAdd ? saveAccount : updateAccount}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
};

export default Account;
