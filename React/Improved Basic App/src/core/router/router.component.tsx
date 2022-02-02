import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { DetailPage, ListPage, LoginPage } from '../../scenes';
import { typedRoutes as routes} from './typed.routes';

export const RouterComponent: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path={routes.list} element={<ListPage />} />
                <Route path={routes.details} element={<DetailPage />} />
                <Route path={routes.root} element={<LoginPage />} />
            </Routes>
        </Router>
    );
};