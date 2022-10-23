import React, { Component } from "react";
import SessionManager from "../Auth/SessionManager";
import { postData } from "../../services/AccessAPI";

export default class Register extends Component {
    constructor(props) {
        super(props);
        this.state = {
            fullName: '',
            email: '',
            userName: '',
            password: '',
            confirmationPassword: '',
            roles: [],
            loading: true,
            fullNameValidationMessage: '',
            emailValidationMessage: '',
            userNameValidationMessage: '',
            passwordValidationMessage: '',
            errorMessage: ''
        };

        this.onSubmit = this.onSubmit.bind(this);
        this.onChange = this.onChange.bind(this);
        this.onClickBack = this.onClickBack.bind(this);

    }

    onSubmit(e) {
        e.preventDefault();
        if(this.validateUser()) {
            const { history } = this.props;
            let userObj = {
                fullName: this.state.fullName,
                email: this.state.email,
                userName: this.state.userName,
                password: this.state.password,
                confirmationPassword: this.state.confirmationPassword,
                roles: []
            }
            postData('api/authentication/register', userObj)
            .then((result) => {
                let responseJson = result;
                if (responseJson) {
                    history.push('/admin/users');
                }
            })
            .catch((error) => {
                
            });
        }
    }
    validateUser() {
        debugger
        this.setState({ fullNameValidationMessage: '', emailValidationMessage: '', userNameValidationMessage: '', passwordValidationMessage: '' });
        var isValid = true;
        if(this.state.fullName.trim() === "" || this.state.fullName.length < 5) {
            this.setState({fullNameValidationMessage: "Full name shouldn't be empty and should be equal or greater than 5 character"}); 
            isValid = false;
        }
        if(/^\w+([\\.-]?\w+)*@\w+([\\.-]?\w+)*(\.\w{2,3})+$/.test(this.state.email) === false) {
            this.setState({emailValidationMessage: "the value should be email"});
            isValid = false;
        }
        if(this.state.userName.trim() === "") {
            this.setState({userNameValidationMessage: "Username shouldn't be empty"});
            isValid = false; 
        }
        if (this.state.password !== this.state.confirmationPassword) {
            this.setState({passwordValidationMessage: "Password and confirm password are not same"}); 
            isValid = false;
        }
        return isValid;
    }
    onClickBack(e){
        e.preventDefault();
        const { history } = this.props;

        if(SessionManager.getToken()){
            history.push('/admin/users');
        } else {
            history.push('/login');
        }   
    }

    onChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }

    render() {
        return (
            <div className="row">
                <div className="col-md-4">
                    <h3>Create new user</h3>
                    <form onSubmit={this.onSubmit}>
                    <div className="text-danger">{this.state.errorMessage}</div>
                        <div className="form-group">
                            <label className="control-label">Full Name: </label>
                            <input className="form-control" type="text" name="fullName" value={this.state.fullName} onChange={this.onChange}></input>
                            <div className="text-danger">{this.state.fullNameValidationMessage}</div>
                        </div>

                        <div className="form-group">
                            <label className="control-label">Email: </label>
                            <input className="form-control" type="text" name="email" value={this.state.email} onChange={this.onChange}></input>
                            <div className="text-danger">{this.state.emailValidationMessage}</div>
                        </div>

                        <div className="form-group">
                            <label className="control-label">User Name: </label>
                            <input className="form-control" type="text" name="userName" value={this.state.userName} onChange={this.onChange}></input>
                            <div className="text-danger">{this.state.userNameValidationMessage}</div>
                        </div>

                        <div className="form-group">
                            <label className="control-label">Password: </label>
                            <input className="form-control" type="password" name="password" value={this.state.password} onChange={this.onChange}></input>
                            <div className="text-danger">{this.state.passwordValidationMessage}</div>
                        </div>

                        <div className="form-group">
                            <label className="control-label">Confirm Password: </label>
                            <input className="form-control" type="password" name="confirmationPassword" value={this.state.confirmationPassword} onChange={this.onChange}></input>
                            <div className="text-danger">{this.state.passwordValidationMessage}</div>
                        </div>


                        <div className="form-group">
                            <input type="submit" value="Create User" className="btn btn-primary"></input> &nbsp; &nbsp; 
                            <input type="button" value="Back" onClick={this.onClickBack} className="btn btn-primary"></input>
                        </div>

                    </form>

                </div>
            </div>
        );
    }
}