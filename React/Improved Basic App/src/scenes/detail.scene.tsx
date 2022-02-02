import React from 'react';
import { Link, useParams } from 'react-router-dom';
import { AppLayout } from '../layouts/app.layout';
import { DetailContainer } from '../pods/detail/detail.container';

export const DetailPage : React.FC = () => {
    const {id} = useParams();
    
    return (
        <AppLayout>
            <DetailContainer id={id} />
        </AppLayout>
    );
};