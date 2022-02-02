import React from "react";
import { MemberEntityApi } from "./list.api.model";

export const GetMemberCollection = ():Promise<MemberEntityApi[]> => {
    return fetch('https://api.github.com/users')
        .then(response => response.json());
};