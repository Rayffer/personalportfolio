import React from 'react';
import { Link, useParams } from 'react-router-dom';

interface MemberDetailEntity {
    id: number;
    login: string;
    name: string;
    company: string;
    bio: string;
}

const createDefaultMemberDetail = (): MemberDetailEntity => ({
    id: 0,
    login: '',
    name: '',
    company: '',
    bio: ''
});

export const DetailPage : React.FC = () => {
    const {id} = useParams();
    const [memberDetail, setMemberDetail] = React.useState(createDefaultMemberDetail());

    React.useEffect(() => {
        fetch(`https://api.github.com/users/${id}`)
            .then(response => response.json())
            .then(data => setMemberDetail(data));
    }, []);
    
    return (
        <div>
            <h2>Detail</h2>
            <h3>User Id: {memberDetail.id}</h3>
            <h3>Login: {memberDetail.login}</h3>
            <h3>Name: {memberDetail.name}</h3>
            <h3>Company: {memberDetail.company}</h3>
            <h3>Bio: {memberDetail.bio}</h3>
            <Link to="/list">Back to the list page</Link>
        </div>
    );
};