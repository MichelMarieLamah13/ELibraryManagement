<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="aboutUs.aspx.cs" Inherits="ELibraryManagement.aboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="about-company-section">
        <div class="container p-1 p-sm-3">
            <div class="row">
                <div class="col-12 text-center">
                    <h2>About ☞  H2S</h2>
                    <hr />
                </div>

                <div class="col-md-3">
                    <img class="img-fluid" src="imgs/logo.png" alt="">
                </div>

                <div class="col-md-9">
                    <p>
                        <strong>H2S</strong> (Here To Serve) is a start up made up of 2 Guinean foreign students in Morocco 
                        and more precisely at the national school of applied science of Tangier in the second year of the engineering cycle
                    </p>
                    <p>
                        Our slogan, work, work and work again ☺
                    </p>
                    <p>
                        In any case, don't forget we are here to serve ❤ ☟ 
                    </p>

                </div>
            </div>
        </div>
    </section>
    <section class="pt-3 pb-4">
        <div class="container">
            <div class="row mt-4">
                <div class="col text-center">
                    <h3>✿ Our awesome team ⚛</h3>
                </div>
            </div>
            <hr>
            <div class="row">
                <div class="col-md-6 text-center">
                    <div class="card" style="width:18rem;">
                        <img src="imgs/team01.jpg" class="card-img-top img-thumbnail" style="height: 200px;"alt="Kadiatou Damariyoun Younoussa Diallo">
                        <div class="card-body">
                            <h5 class="card-title">Diallo K.Y.D</h5>
                            <h6 class="card-subtitle mb-2 text-muted">Elève Ingénieure - 4 ème année</h6>
                            <h6 class="card-subtitle mb-2 text-muted">Génie Informatique</h6>
                            <h6 class="card-subtitle mb-2 text-muted">Ensa Tanger</h6>
                            <hr />
                            <a href="mailto:kadiatouyoun.diallo@gmail.com" class="card-link">kadiatouyoun.diallo@gmail.com</a><br />
                            <a href="tel:+212626981842" class="card-link">06-26-98-18-42</a>
                        </div>
                    </div>

                </div>
                <div class="col-md-6 text-center">
                   <div class="card float-right" style="width:18rem;">
                        <img src="imgs/team02.jpg" class="card-img-top img-thumbnail" style="height: 200px;" alt="Michel Marie Lamah">
                        <div class="card-body">
                            <h5 class="card-title">Lamah M.M</h5>
                            <h6 class="card-subtitle mb-2 text-muted">Elève Ingénieure - 4 ème année</h6>
                            <h6 class="card-subtitle mb-2 text-muted">Génie Informatique</h6>
                            <h6 class="card-subtitle mb-2 text-muted">Ensa Tanger</h6>
                            <hr />
                            <a href="mailto:lamahmichelmarie@gmail.com" class="card-link">lamahmichelmarie@gmail.com</a><br />
                            <a href="tel:+212622757768" class="card-link">06-22-75-77-68</a>
                        </div>
                    </div>

                </div>
            </div>
    </section>

</asp:Content>
