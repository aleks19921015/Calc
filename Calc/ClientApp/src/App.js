import React, { Component } from 'react';
import { Route } from 'react-router';

import './custom.css'
import {Calculator} from "./components/Calculator.tsx";

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Route exact path='/' component={Calculator} />
    );
  }
}
