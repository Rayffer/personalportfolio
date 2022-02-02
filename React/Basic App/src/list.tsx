import React from 'react';
import { Link } from 'react-router-dom';

interface MemberEntity {
    id: number;
    login: string;
    avatar_url: string;
}

export const ListPage: React.FC = () => {
    const [members, setMembers] = React.useState<MemberEntity[]>([]);

    React.useEffect(() => {
        fetch('https://api.github.com/users')
            .then(response => response.json())
            .then(data => setMembers(data));
    }, []);

    return (
        <div>
            <h2>List</h2>
            <div className="list-user-list-container">

                <span className="list-header">Avatar</span>
                <span className="list-header">Id</span>
                <span className="list-header">Name</span>
                {members.map((member) => (
                    <>
                        <img src={member.avatar_url} />
                        <span>{member.id}</span>
                        <span><Link to={`/detail/${member.login}`}>{member.login}</Link></span>
                    </>
                ))}
            </div>
            <Link to="/detail">Detail</Link>
            <br />

            <Link to="/">Back to the login page</Link>
        </div>
    );
};