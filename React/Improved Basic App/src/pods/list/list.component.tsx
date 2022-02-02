import React from "react";
import { Link } from "react-router-dom";
import { routes } from "../../core";
import { MemberEntity } from "./list.vm";

interface Props {
    members: MemberEntity[]
}

export const ListComponent: React.FC<Props> = (props) => {
    const { members } = props;

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
                        <Link to={routes.details(member.login)}>{member.login}</Link>
                    </>
                ))}
            </div>
        </div>
    );
};