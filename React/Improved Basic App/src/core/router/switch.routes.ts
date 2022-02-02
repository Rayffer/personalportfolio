import {generatePath} from "react-router-dom";

interface SwitchRoutes {
    root: string;
    list: string;
    details: string;
}

export const switchRoutes : SwitchRoutes = {
    root: "/",
    list: "/list",
    details: "/detail/:id"
};

interface Routes extends Omit<SwitchRoutes, "details"> {
    details: (id: string) => string;
}

export const routes : Routes = {
    root: "/",
    list: "/list",
    details: (id)=> `/detail/${id}`
};