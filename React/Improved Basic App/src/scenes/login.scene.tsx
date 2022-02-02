import React from 'react';
import { CenterLayout } from '../layouts';
import { LoginContainer } from '../pods/login';

export const LoginPage: React.FC = () => {

    return (
        <CenterLayout>
            <LoginContainer />
        </CenterLayout>
    );
};