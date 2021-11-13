/* eslint-disable react/jsx-props-no-spreading */
import { ReactNode } from 'react';
import { Route, Redirect } from 'react-router-dom';

const GuardedRoute = ({
    children,
    authorized,
    redirectPath,
    ...rest
}: {
    children: ReactNode;
    authorized: boolean;
    redirectPath: string;
    [x: string]: any;
}) => <Route {...rest}>{authorized === true ? children : <Redirect to={redirectPath} />}</Route>;

export default GuardedRoute;
