import React from "react";
import { ListComponent } from "./list.component";

export const ListContainer: React.FC = () => {
    const [members, setMembers] = React.useState<MemberEntity[]>([]);

    React.useEffect(() => {
        fetch('https://api.github.com/users')
            .then(response => response.json())
            .then(data => setMembers(data));
    }, []);

    return <ListComponent members={members} />;
};
    