import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import Login from './components/Auth/Login';
import Registration from './components/Auth/Registration';  
import Logout from './components/Auth/Logout';
import Account from './components/Account';    

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/login' component={Login} />
        <Route path='/register' component={Registration} /> 
        <Route path='/logout' component={Logout}/>
        <Route path='/account' component={Account} />
      </Layout>
    );
  }
}
