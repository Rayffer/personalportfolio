import React from "react";
import { DetailComponent } from "./detail.component";
import { MemberDetailEntity, createDefaultMemberDetail } from "./detail.vm";

interface Props {
    id: string;
}

export const DetailContainer: React.FC<Props> = (props) => {
    const {id} = props;
    const [member, setMember] = React.useState<MemberDetailEntity>(createDefaultMemberDetail());

    React.useEffect(() => {
        fetch(`https://api.github.com/users/${id}`)
            .then(response => response.json())
            .then(data => setMember(data));
    }, []);

    return <DetailComponent member={member} />;
};