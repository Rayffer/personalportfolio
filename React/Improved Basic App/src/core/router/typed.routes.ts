import { generatePath} from "react-router-dom";

type NavigationFn = (id: string) => string;

interface BaseRoutes {
  root: string;
  list: string;
  details: string | NavigationFn;
}

interface TypedRoutes extends BaseRoutes {
  details: string;
}

interface Routes extends BaseRoutes {
  details: NavigationFn;
}

export const typedRoutes: TypedRoutes = {
  root: "/",
  list: "/list",
  details: "/detail/:id",
};

export const routes: Routes = {
  ...typedRoutes,
  details: (id) => `/detail/${id}`,
};