import React from 'react';
import { AppLayout } from '../layouts/app.layout';
import { ListContainer } from '../pods/list';

export const ListPage: React.FC = () => {
    
    return (
        <AppLayout>
            <ListContainer />
        </AppLayout>
    );
};