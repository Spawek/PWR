function [ weights_matrix, A_matrix, r, PCAd_probes] = calc_PCAd_RBF_weight_matrix( training_data, BACKWARD_DATA_USAGE )

%% constants block
PCA_CERTAINITY_LEVEL = 0.9999999;% 0.9997; % WTF? - i need certainity level like that to have 15 probes out of 30

%% probes preparation
probes = generate_probes(training_data, BACKWARD_DATA_USAGE);

%% PCA
[PCAd_probes, A_matrix] = cut_dimensions(probes, PCA_CERTAINITY_LEVEL); 

disp(['PCA is cutting dimensions from ', ...
    num2str(size(A_matrix, 1)), ' to ', ...
    num2str(size(A_matrix, 2)) ])

%% learning
[weights_matrix, r] = calc_weights_matrix(PCAd_probes);

end

